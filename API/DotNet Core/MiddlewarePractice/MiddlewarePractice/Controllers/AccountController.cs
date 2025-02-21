using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiddlewarePractice.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiddlewarePractice.Controllers
{
    /// <summary>
    /// Controller for account-related actions such as login and logout.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates the user and generates a JWT token.
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials.</param>
        /// <returns>An action result containing the JWT token if authentication is successful.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // In a real-world scenario, replace with proper user authentication
            if (loginModel.Username == "admin@example.com" && loginModel.Password == "Admin123")
            {
                string? token = GenerateJwtToken(loginModel.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        /// <summary>
        /// Logs out the user.
        /// </summary>
        /// <returns>An action result indicating the logout status.</returns>
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Implement token invalidation logic if needed
            return Ok(new { Message = "Logged out successfully" });
        }

        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        /// <param name="username">The username of the authenticated user.</param>
        /// <returns>A JWT token as a string.</returns>
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