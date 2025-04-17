using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AccessManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//using System.Security.Cryptography;


//Console.WriteLine();
//Console.WriteLine($"JWT Secret Key Length: {secretKey.Length}"); // This should print 32
//Console.ReadLine();


//var randomKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)); // 256-bit key (32 bytes)


builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowAnyOrigin", policy =>
    //{
    //    policy.AllowAnyOrigin()  // Allow any origin
    //          .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
    //          .AllowAnyHeader();  // Allow any HTTP header
    //});

    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5000")  // Allow only localhost:50000 as origin
              .AllowAnyMethod()                      // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader()                      // Allow any HTTP header
              .AllowCredentials();                   // Allow credentials (cookies, HTTP authentication, etc.)
    });

});






builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
             ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully");
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddScoped<Auth>();
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
