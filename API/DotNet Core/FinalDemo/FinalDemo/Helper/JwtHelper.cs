using FinalDemo.Models.POCO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalDemo.Helpers
{
    /// <summary>
    /// Helper class for generating and validating JWT tokens.
    /// </summary>
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtHelper"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JWT token for the specified user and role.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <param name="role">The role of the user.</param>
        /// <returns>A JWT token as a string.</returns>
        public string GenerateJwtToken(YMU01 user, string role)
        {
            Claim[] claims = new[]
            {
                new Claim("userId", user.U01F01.ToString()), // Store userId in claims
                new Claim(ClaimTypes.Email, user.U01F03),
                new Claim(ClaimTypes.Role, role),
            };

            SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            SigningCredentials? creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken? token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Extracts the userId from a ClaimsPrincipal (e.g., from HttpContext.User).
        /// </summary>
        /// <param name="user">ClaimsPrincipal representing the authenticated user.</param>
        /// <returns>The userId as an integer, or null if not found.</returns>
        public int? GetUserIdFromClaims(ClaimsPrincipal user)
        {
            Claim? userIdClaim = user?.FindFirst("userId"); // Extract userId from claims
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null; // Return null if userId is not found or not valid
        }

        /// <summary>
        /// Extracts the email from a ClaimsPrincipal (e.g., from HttpContext.User).
        /// </summary>
        /// <param name="user">ClaimsPrincipal representing the authenticated user.</param>
        /// <returns>The email as a string, or null if not found.</returns>
        public string? GetEmailFromClaims(ClaimsPrincipal user)
        {
            Claim? emailClaim = user?.FindFirst(ClaimTypes.Email);
            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
            return null; // Return null if email is not found
        }

        /// <summary>
        /// Extracts the role from a ClaimsPrincipal (e.g., from HttpContext.User).
        /// </summary>
        /// <param name="user">ClaimsPrincipal representing the authenticated user.</param>
        /// <returns>The role as a string, or null if not found.</returns>
        public string? GetRoleFromClaims(ClaimsPrincipal user)
        {
            Claim? roleClaim = user?.FindFirst(ClaimTypes.Role);
            if (roleClaim != null)
            {
                return roleClaim.Value;
            }
            return null; // Return null if role is not found
        }
    }
}