using FilterPractice.Filters;
using FilterPractice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilterPractice.Controllers
{
    /// <summary>
    /// Controller for handling user authentication-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration settings.</param>
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates the user and returns a JWT token if valid.
        /// </summary>
        /// <param name="loginModel">User login credentials.</param>
        /// <returns>A JWT token if authentication is successful; otherwise, Unauthorized.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // In a real-world scenario, replace with proper user authentication
            if (loginModel.Username == "admin@example.com" && loginModel.Password == "Admin123")
            {
                var token = GenerateJwtToken(loginModel.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        /// <summary>
        /// Logs out the user (Token invalidation logic can be implemented).
        /// </summary>
        /// <returns>A success message indicating the user has logged out.</returns>
        [HttpPost("logout")]
        [CustomAuthorizeAttribute]
        public IActionResult Logout()
        {
            // Implement token invalidation logic if needed
            return Ok(new { Message = "Logged out successfully" });
        }

        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        /// <param name="username">The username of the authenticated user.</param>
        /// <returns>A JWT token string.</returns>
        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
