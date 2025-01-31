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
    /// <summary>
    /// Controller responsible for user authentication, including login functionality.
    /// </summary>
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IOrmLiteDbFactory _ormLiteDbFactory;
        private readonly IDbConnection _dbFactory;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor to initialize AuthController with necessary dependencies.
        /// </summary>
        /// <param name="configuration">Application configuration for retrieving JWT settings.</param>
        /// <param name="ormLiteDbFactory">Factory to handle database connection.</param>
        public AuthController(IConfiguration configuration, IOrmLiteDbFactory ormLiteDbFactory)
        {
            _configuration = configuration;
            _ormLiteDbFactory = ormLiteDbFactory;
            _dbFactory = _ormLiteDbFactory.OpenConnection();
        }

        /// <summary>
        /// Endpoint for user login. Validates user credentials and returns a JWT token.
        /// </summary>
        /// <param name="loginRequest">The login request containing email and password.</param>
        /// <returns>Returns JWT token if login is successful; Unauthorized if credentials are invalid.</returns>
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

        /// <summary>
        /// Helper method to generate a JWT token for the user.
        /// </summary>
        /// <param name="email">User's email to include in the token claim.</param>
        /// <param name="role">Role of the user to be included in the token claim.</param>
        /// <returns>A JWT token string.</returns>
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

    /// <summary>
    /// Data transfer object (DTO) for the login request containing email and password.
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
