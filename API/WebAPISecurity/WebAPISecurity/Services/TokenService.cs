using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPISecurity.Models;

namespace WebAPISecurity.Services
{
    public class TokenService
    {
        private const string SecretKey = "1234567890abcdef1234567890abcdef"; // Use a strong secret key
        private static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        private static readonly SigningCredentials Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

        // Generate JWT Token
        public static string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("sub", user.Username),
                new Claim("email", user.Email),
                new Claim("userId", user.UserId.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "AuthService",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Validate JWT Token
        public static ClaimsPrincipal ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "AuthService",
                ValidAudience = "your_audience",
                IssuerSigningKey = Key
            };

            return tokenHandler.ValidateToken(token, parameters, out _);
        }
    }

}