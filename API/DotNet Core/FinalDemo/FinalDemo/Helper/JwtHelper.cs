using FinalDemo.Models.POCO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalDemo.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(YMU01 user, string role)
        {
            Claim[] claims = new[]
            {
                new Claim("userId", user.U01F01.ToString()), // Store userId in claims
                new Claim(ClaimTypes.Email, user.U01F03),
                new Claim(ClaimTypes.Role, role),
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

        /// <summary>
        /// Extracts the userId from a ClaimsPrincipal (e.g., from HttpContext.User)
        /// </summary>
        /// <param name="user">ClaimsPrincipal representing the authenticated user</param>
        /// <returns>The userId as an integer, or null if not found</returns>
        public int? GetUserIdFromClaims(ClaimsPrincipal user)
        {
            var userIdClaim = user?.FindFirst("userId"); // Extract userId from claims
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return null; // Return null if userId is not found or not valid
        }
    }
}
