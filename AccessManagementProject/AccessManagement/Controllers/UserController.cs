using Microsoft.AspNetCore.Mvc;
using AccessManagement.Data;  
using AccessManagement.Models;  
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
//using System.Net.Mail;
using MailKit.Net.Smtp;
//using MailKit.Security;
using MimeKit;
using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Asn1.X509;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.AspNetCore.Identity.Data;
using AccessManagement.security;



namespace AccessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string smtpUser = "samirpadghane@gmail.com";
        private readonly string smtpPassword = "axgnozqovgxgfuqv";
        private readonly string fromEmail = "samirpadghane@gmail.com";

        private readonly AppDBContext _dbContext;
        private readonly Auth _auth;

        public UserController(AppDBContext cc, Auth auth)
        {
            this._dbContext = cc;
            _auth = auth;
        }
        [HttpPost("api/createUser")]
        public async Task<ActionResult<User>> createUser()
        {
            // Retrieve required header values.
            if (!Request.Headers.TryGetValue("X-Name", out var nameHeader) ||
                !Request.Headers.TryGetValue("X-Username", out var usernameHeader) ||
                !Request.Headers.TryGetValue("X-Id", out var idHeader) ||
                !Request.Headers.TryGetValue("X-Password", out var passwordHeader) ||
                !Request.Headers.TryGetValue("X-Role", out var roleHeader) ||
                !Request.Headers.TryGetValue("X-Email", out var emailHeader))
            {
                return BadRequest(new { message = "Missing required headers." });
            }

            // Convert header values to proper types and rename local variables if needed.
            string Name = nameHeader.ToString();
            string username = usernameHeader.ToString();
            string idString = idHeader.ToString();
            string password = passwordHeader.ToString();
            string role = roleHeader.ToString();
            // Rename local variable to avoid conflict (if any other variable named email exists)
            string emailVal = emailHeader.ToString();

            if (!int.TryParse(idString, out int id))
            {
                return BadRequest(new { message = "Invalid id." });
            }

            // Create the new user object.
            User user = new User
            {
                id = id,
                name = Name,
                username = username,
                //password = password,
                role = role
            };
            string publicKey = RsaEncryption.GetPublicKey();
            user.password = RSAEncryptPassword.EncryptPassword(password, publicKey);

            // (The remaining logic, such as reading JWT, checking roles, validation, etc.)
            // ...

            // For example:
            var tt = Request.Cookies["jwtToken"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tt) as JwtSecurityToken;

            var tokenNameClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (tokenNameClaim == null)
            {
                return Unauthorized(new { message = "Invalid token." });
            }
            var tokenUsername = tokenNameClaim.Value;

            var ccuser = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == tokenUsername);

            if (ccuser == null || (ccuser.role != "admin" && ccuser.role != "manager"))
            {
                return Conflict(new { message = "You don't have access to perform this operation." });
            }

            var existingUser = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == user.username);
            if (existingUser != null)
            {
                return Conflict(new { message = "User with this username already exists." });
            }

            if (password.Length < 10)
            {
                return Conflict(new { message = "Password must be of 10 characters." });
            }

            int f1 = 0, f2 = 0, f3 = 0, f4 = 0;
            foreach (char ch in password)
            {
                if (ch >= 'A' && ch <= 'Z') f1++;
                else if (ch >= '0' && ch <= '9') f2++;
                else if (ch >= 'a' && ch <= 'z') f3++;
                else f4++;
            }

            if (f1 == 0)
                return Conflict(new { message = "Password must contain at least one uppercase letter." });
            if (f2 == 0)
                return Conflict(new { message = "Password must contain a numeric value." });
            if (f3 == 0)
                return Conflict(new { message = "Password must contain at least one lowercase letter." });
            if (f4 == 0)
                return Conflict(new { message = "Password must contain at least one special character." });

            _dbContext.user.Add(user);
            await _dbContext.SaveChangesAsync();

            UserEmail dd = new UserEmail
            {
                username = username,
                otp = "-1",
                email = emailVal,
                question = "default",
                answer = "default",
                validity = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };

            _dbContext.userEmail.Add(dd);
            await _dbContext.SaveChangesAsync();

            var tokenNew = _auth.GenerateJwtToken(user.username);

            var newAudit = new AuditLog
            {
                User = ccuser.username,
                action = "Created user " + user.username,
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();

            var response = new
            {
                User = user,
                message = "User created successfully",
                Token = tokenNew
            };

            return Ok(response);
        }


        [HttpPost("api/login/")]
        public async Task<ActionResult<User>> LoginUser()
        {
            // Retrieve credentials from headers (using custom header names)
            if (!Request.Headers.TryGetValue("X-Username", out var usernameHeader) ||
                !Request.Headers.TryGetValue("X-Password", out var passwordHeader))
            {
                // If either header is missing, return a bad request response.
                return BadRequest(new { message = "Missing credentials in headers." });
            }

            // Convert header values to string.
            string username = usernameHeader.ToString();
            string password = passwordHeader.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Username and password cannot be empty." });
            }

            // Look up the user in the database.
            var u1 = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == username);
            if (u1 == null)
            {
                return Conflict(new { message = "User with this username does not exist." });
            }

            // Check the password (for a production system you should use hashed passwords!)
            //if (u1.password != password)
            //{
            //    return Conflict(new { message = "Password is incorrect." });
            //}
            string privateKey = RsaEncryption.GetPrivateKey();
            string decryptedPassword = RSADecryptPassword.DecryptPassword(u1.password, privateKey);
            // Generate a JWT token and set it as an HTTP-only cookie.
            var token = _auth.GenerateJwtToken(username);
            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = false
            });

            // Optionally log the login action.
            var newAudit = new AuditLog
            {
                User = username,
                action = "Logged in",
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };
            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();

            // Return a successful login message.
            return Ok(new { message = "User logged in successfully" });
        }

        //[HttpPost("login-2fa")]
        ////public async Task<IActionResult> LoginWithOTP(string code, string username)
        ////{

        ////}

        [HttpGet("api/getcurrentuser")]
        

        public async Task<ActionResult<User>> getCurrentUser()
        {

            var token = Request.Cookies["jwtToken"];

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if(jsonToken != null)
            {
                var username = jsonToken.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;


                if (username == null)
                {
                    var res = new
                    {
                        message = "user already exists"
                    };
                    return Conflict(res);

                }
                else
                {
                    var user = await _dbContext.user.FirstOrDefaultAsync(ele=>ele.username==username);
                    var res1 = new
                    {
                        User=user,
                        
                        message = "user fetched successfully"
                    };
                    return Ok(res1);


                }
            }




            return Ok();
        }

        [HttpGet("api/logout")]

        public async Task<ActionResult<User>> LogoutUser()
        {
            var token = Request.Cookies["jwtToken"];

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var username = jsonToken.Claims?.FirstOrDefault(ele=>ele.Type==ClaimTypes.Name)?.Value;
            Response.Cookies.Delete("jwtToken");

            var res = new
            {
                message = "user logged out successfully"
            };

            var newAudit = new AuditLog
            {
                User = username,
                action = "Logged out  ",
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };

            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();
            return Ok(res);
        }


        [HttpPost("api/deleteuser")]
        public async Task<ActionResult<User>> Deleteuser()
        {
            // Retrieve username from header.
            if (!Request.Headers.TryGetValue("X-Username", out var usernameHeader))
            {
                return BadRequest(new { message = "Missing required header: X-Username" });
            }
            string username = usernameHeader.ToString();

            // Retrieve JWT token from cookies.
            var tt = Request.Cookies["jwtToken"];
            if (string.IsNullOrEmpty(tt))
            {
                return Unauthorized(new { message = "No token provided." });
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tt) as JwtSecurityToken;
            var nameClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (nameClaim == null)
            {
                return Unauthorized(new { message = "Invalid token." });
            }
            var tokenUsername = nameClaim.Value;

            // Get the current user (the one making the request).
            var ccuser = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == tokenUsername);
            if (ccuser == null || (ccuser.role != "admin" && ccuser.role != "manager"))
            {
                return Conflict(new { message = "You don't have access to perform this operation" });
            }

            // Find the user to delete.
            var user = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == username);
            if (user == null)
            {
                return Conflict(new { message = "User does not exist" });
            }

            // Delete the user.
            _dbContext.user.Remove(user);
            await _dbContext.SaveChangesAsync();

            // Log the deletion.
            var newAudit = new AuditLog
            {
                User = ccuser.username,
                action = "Deleted user " + username,
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };
            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();

            var res = new { message = "User deleted successfully" };
            return Ok(res);
        }


        [HttpPost("api/changepassword")]
        public async Task<ActionResult<User>> ChangePassword()
        {
            // Retrieve the new password from the header "X-NewPassword"
            if (!Request.Headers.TryGetValue("X-NewPassword", out var newPasswordHeader))
            {
                return BadRequest(new { message = "Missing required header: X-NewPassword" });
            }
            string newPassword = newPasswordHeader.ToString();

            // Password validation
            if (newPassword.Length < 10)
            {
                return Conflict(new { message = "Password must be of 10 characters" });
            }

            int f1 = 0, f2 = 0, f3 = 0, f4 = 0;
            for (int i = 0; i < newPassword.Length; i++)
            {
                char ch = newPassword[i];
                if (ch >= 'A' && ch <= 'Z')
                    f1++;
                else if (ch >= '0' && ch <= '9')
                    f2++;
                else if (ch >= 'a' && ch <= 'z')
                    f3++;
                else
                    f4++;
            }

            if (f1 == 0)
                return Conflict(new { message = "Password must contain at least 1 uppercase letter" });
            if (f2 == 0)
                return Conflict(new { message = "Password must contain a numeric value" });
            if (f3 == 0)
                return Conflict(new { message = "Password must contain at least 1 lowercase letter" });
            if (f4 == 0)
                return Conflict(new { message = "Password must contain a special character" });

            // Retrieve JWT token from cookies
            var tt = Request.Cookies["jwtToken"];
            if (string.IsNullOrEmpty(tt))
            {
                return Unauthorized(new { message = "No token provided" });
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tt) as JwtSecurityToken;
            var nameClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (nameClaim == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            string tokenUsername = nameClaim.Value;

            // Retrieve the current user from the database
            var ccuser = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == tokenUsername);
            if (ccuser == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            // Change the password
            ccuser.password = newPassword;
            await _dbContext.SaveChangesAsync();

            // Log the change
            var newAudit = new AuditLog
            {
                User = ccuser.username,
                action = "Changed the password",
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };
            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();

            var res = new { message = "Password changed successfully" };
            return Ok(res);
        }



        [HttpPost("api/changepassworduser")]

        public async Task<ActionResult<User>> ChangeUserPassword(string username, string password)
        {
            var token = Request.Cookies["jwtToken"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var uname =  jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            var ccuser = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == uname);

            if (ccuser == null)
            {
                var res = new
                {
                    message = "user not getting"
                };

                return Conflict(res);
            }

            if(ccuser.role != "admin" && ccuser.role != "manager")
            {
                var res = new
                {
                    message = "You don't have an access to change password"
                };

                return Conflict(res);
            }

            var userRes = await _dbContext.user.FirstOrDefaultAsync(ele=> ele.username==username);

            if (userRes == null)
            {
                var res = new
                {
                    message = "user with this username not exists"
                };

                return Conflict(res);
            }

            var pass = password;
            if (pass.Length < 10)
            {
                var res1 = new
                {
                    message = "password must be of 10 characters"
                };

                return Conflict(res1);

            }

            int f1 = 0;
            int f2 = 0;
            int f3 = 0;
            int f4 = 0;


            for (int i = 0; i < pass.Length; i++)
            {
                char ch = pass[i];

                if (ch >= 'A' && ch <= 'Z')
                {
                    f1++;
                }
                else if (ch >= '0' && ch <= '9')
                {
                    f2++;
                }
                else if (ch >= 'a' && ch <= 'z')
                {
                    f3++;
                }
                else
                {
                    f4++;
                }
            }

            if (f1 == 0)
            {
                var res1 = new
                {
                    message = "password must contain 1 Uppercase letter"
                };

                return Conflict(res1);

            }

            if (f2 == 0)
            {
                var res1 = new
                {
                    message = "password must contain a numeric value"
                };

                return Conflict(res1);

            }

            if (f3 == 0)
            {
                var res1 = new
                {
                    message = "password must contain 1 Lowercase letter"
                };

                return Conflict(res1);

            }

            if (f4 == 0)
            {
                var res2 = new
                {
                    message = "password must contain a special character"
                };

                return Conflict(res2);
            }

                userRes.password = password;
            await _dbContext.SaveChangesAsync();

            var res10 = new
            {
                message = "Password changed successfully"
            };
            var newAudit = new AuditLog
            {
                User = ccuser.username,
                action = "changed the password of :"+uname,
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };
            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();
            return Ok(res10);

            //var ccuser = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == name);

            

            //return Ok();
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(string username, string question, string answer)
        {
            if (string.IsNullOrEmpty(username))
            {
                var res123 = new
                {
                    message = "Username cannot be null or empty"
                };
                return Conflict(res123);
            }
            var useremail = await _dbContext.userEmail.FirstOrDefaultAsync(ele => ele.username == username);
            if (useremail == null)
            {
                var res123 = new
                {
                    message = "User with this username does not exist"
                };
                return Conflict(res123);
            }
            if (question != useremail.question)
            {
                var res12 = new
                {
                    message = "wrong question."
                };
                return Conflict(res12);
            }
            if(answer != useremail.answer)
            {
                var res13 = new
                {
                    message = "wrong answer."
                };
                return Conflict(res13);
            }
            var recipientEmail = useremail.email;
            try
            {
                Random rand = new Random();
                int randomNumber = rand.Next(100000, 1000000);
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("myapp", fromEmail));
                emailMessage.To.Add(new MailboxAddress("Recipient", recipientEmail));
                emailMessage.Subject = "Test Email from ASP.NET Core";
                var bodyBuilder = new BodyBuilder { TextBody = $"This is a Your OTP for password reset {randomNumber}.\nThis otp is valid for 30 minutes." };
                emailMessage.Body = bodyBuilder.ToMessageBody();
                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(smtpServer, smtpPort, false);
                    await smtpClient.AuthenticateAsync(smtpUser, smtpPassword);
                    await smtpClient.SendAsync(emailMessage);
                    await smtpClient.DisconnectAsync(true);
                }
                useremail.otp = randomNumber.ToString();
                useremail.validity= DateTime.UtcNow.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss");
                await _dbContext.SaveChangesAsync();
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("api/check-otp")]
        public async Task<ActionResult<UserEmail>> checkotp(string username, string otp)
        {
            var useremail = await _dbContext.userEmail.FirstOrDefaultAsync(ele => ele.username == username);
            DateTime dateTime = DateTime.Parse(useremail.validity);
            if (useremail.otp == otp && dateTime> DateTime.UtcNow)
            {
                var res = new
                {
                    message = "you can reset your password"
                };
                return Ok(res);
            }
            else
            {
                var res = new
                {
                    message = "incorrect otp or the otp has expired"
                };
                return Conflict(res);
            }
            //return Ok();
        }
        [HttpPost("api/resetpassword")]
        public async Task<ActionResult<User>> ResetPassword(string username, string newPassword)
        {
            var user = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == username);
            var pass = newPassword;
            if (pass.Length < 10)
            {
                var res1 = new
                {
                    message = "password must be of 10 characters"
                };
                return Conflict(res1);
            }
            int f1 = 0;
            int f2 = 0;
            int f3 = 0;
            int f4 = 0;
            for (int i = 0; i < pass.Length; i++)
            {
                char ch = pass[i];
                if (ch >= 'A' && ch <= 'Z')
                {
                    f1++;
                }
                else if (ch >= '0' && ch <= '9')
                {
                    f2++;
                }
                else if (ch >= 'a' && ch <= 'z')
                {
                    f3++;
                }
                else
                {
                    f4++;
                }
            }
            if (f1 == 0)
            {
                var res1 = new
                {
                    message = "password must contain 1 Uppercase letter"
                };
                return Conflict(res1);
            }
            if (f2 == 0)
            {
                var res1 = new
                {
                    message = "password must contain a numeric value"
                };
                return Conflict(res1);
            }
            if (f3 == 0)
            {
                var res1 = new
                {
                    message = "password must contain 1 Lowercase letter"
                };
                return Conflict(res1);
            }
            if (f4 == 0)
            {
                var res1 = new
                {
                    message = "password must contain a special character"
                };
                return Conflict(res1);
            }
            if (user == null)
            {
                var res = new
                {
                    message = "user with this username not exist"
                };
                return Conflict(res);
            }
            user.password = newPassword;
            await _dbContext.SaveChangesAsync();
            var res10 = new
            {
                message = "Password has changed successfully"
            };
            var newAudit = new AuditLog
            {
                User = user.username,
                action = "changed the password",
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };
            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();
            return Ok(res10);
        }

        [HttpPost("question")]
        public async Task<ActionResult<UserEmail>> SecQue()
        {
            // Read the required values from headers
            if (!Request.Headers.TryGetValue("X-Username", out var usernameHeader) ||
                !Request.Headers.TryGetValue("X-Question", out var questionHeader) ||
                !Request.Headers.TryGetValue("X-Answer", out var answerHeader))
            {
                return BadRequest(new { message = "Missing required headers." });
            }

            string username = usernameHeader.ToString();
            string question = questionHeader.ToString();
            string answer = answerHeader.ToString();

            // Look up the userEmail record by username
            var user = await _dbContext.userEmail.FirstOrDefaultAsync(ele => ele.username == username);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Update the security question and answer
            user.question = question;
            user.answer = answer;
            await _dbContext.SaveChangesAsync();

            // Log audit data
            var newAudit = new AuditLog
            {
                User = user.username,
                action = "Update security question",
                TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
            };
            await _dbContext.logs.AddAsync(newAudit);
            await _dbContext.SaveChangesAsync();

            var res = new { message = "Data Saved!" };
            return Ok(res);
        }

    }
}
