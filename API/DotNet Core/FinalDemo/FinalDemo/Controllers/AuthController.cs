using FinalDemo.Interfaces;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.OrmLite;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalDemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IOrmLiteDbFactory _ormLiteDbFactory;
        private readonly IDbConnection _dbFactory;
        private readonly IConfiguration _configuration;

        // Constructor
        public AuthController(IConfiguration configuration, IOrmLiteDbFactory ormLiteDbFactory)
        {
            _configuration = configuration;
            _ormLiteDbFactory = ormLiteDbFactory;
            _dbFactory = _ormLiteDbFactory.OpenConnection();
        }

        // Login Endpoint
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Validate user credentials
            List<YMU01> user = _dbFactory.Select<YMU01>(u => u.U01F03 == loginRequest.Email && u.U01F04 == loginRequest.Password);

            if (user == null || !user.Any()) 
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            return Ok(new
            {
                Token = GenerateJwtToken(loginRequest.Email, "user") // Generate token
            });
        }

        // Helper method to generate JWT token
        private string GenerateJwtToken(string email, string role)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Return the JWT as string
        }
    }

    // DTO for Login request (better approach)
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
