using Microsoft.AspNetCore.Mvc;
using AccessManagement.Data;
using AccessManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AccessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase 
    {
        private readonly AppDBContext _dbContext;
        private readonly Auth auth;
        public AuditController(AppDBContext dbContext, Auth auth)
        {
            _dbContext = dbContext;
            this.auth = auth;
        }

        [HttpGet("api/getAllLogs")]

        public async Task<ActionResult<AuditLog>> getAllLogs()
        {
            var token = Request.Cookies["jwtToken"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken=handler.ReadToken(token) as JwtSecurityToken;

            var username=jsonToken.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.Name)?.Value;

            var user = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == username);

            if (user.role != "manager" && user.role != "admin")
            {
                var res1 = new
                {
                    meassage = "you don't have access to fetch logs"
                };

                return Conflict(res1);
            }

            var logs = await _dbContext.logs.ToListAsync();

            var res = new
            {
                message = "logs fetched successfully",
                logs
            };



            
            return Ok(res);
        }


        [HttpGet("api/getlogsofuser")]

        public async Task<ActionResult<AuditLog>> getLogsOfUser(string username)
        {

            var token = Request.Cookies["jwtToken"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var un = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var user = await _dbContext.user.FirstOrDefaultAsync(ele => ele.username == un);

            if (user.role != "manager" && user.role != "admin")
            {
                var res1 = new
                {
                    meassage = "you don't have access to fetch logs"
                };

                return Conflict(res1);
            }

            var logs = await _dbContext.logs.Where(log=> log.User==username).ToListAsync();

            var res = new
            {
                message = "logs fetched successfully",
                logs
            };




            return Ok(res);

            
        }
    }

}
