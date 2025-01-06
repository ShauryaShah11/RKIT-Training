using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIFinalDemo.Models;

namespace WebAPIFinalDemo.Services
{
    /// <summary>
    /// The TokenService class is responsible for generating and verifying JSON Web Tokens (JWT).
    /// It provides functionality to create secure tokens used for authentication and authorization,
    /// as well as to validate tokens to ensure their integrity and authenticity. The service ensures
    /// that the tokens are properly signed, contain the necessary claims, and have not been tampered with.
    /// </summary>
    public class TokenService
    {
        #region Private Members
        // Use a strong secret key for signing the JWT token
        private const string SecretKey = "1234567890abcdef1234567890abcdef"; // Use a strong secret key
        private static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        private static readonly SigningCredentials Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates a JWT token for the given user with claims such as username, email, and user ID.
        /// The token is signed using HMAC SHA256 algorithm and includes expiration time.
        /// </summary>
        /// <param name="user">The user for whom the JWT token is being generated.</param>
        /// <returns>A JWT token as a string.</returns>
        public static string GenerateToken(User user)
        {
            // Define the claims to be included in the JWT token
            Claim[] claims = new Claim[]
            {
                new Claim("sub", user.Name),    // Subject (username)
                new Claim("email", user.Email),      // Email
                new Claim("userId", user.UserId.ToString()) // UserId
            };

            // Create a JWT token with issuer, audience, claims, expiration, and signing credentials
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "AuthService",                // The service issuing the token
                audience: "your_audience",            // The intended recipient of the token
                claims: claims,                       // Claims related to the user
                expires: DateTime.UtcNow.AddDays(1),  // Set the expiration time for 1 day
                signingCredentials: Credentials      // Signing credentials for token integrity
            );

            // Return the serialized JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates a given JWT token by checking its signature, issuer, audience, and expiration.
        /// If the token is valid, it returns the claims principal, otherwise, it throws an exception.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>A ClaimsPrincipal containing the claims from the valid token.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the token is invalid or expired.</exception>
        public static ClaimsPrincipal ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,                // Validate the token's issuer
                ValidateAudience = true,              // Validate the token's audience
                ValidateLifetime = true,              // Validate the token's expiration time
                ValidIssuer = "AuthService",          // The expected issuer of the token
                ValidAudience = "your_audience",     // The expected audience of the token
                IssuerSigningKey = Key                // The key used for verifying the token's signature
            };

            try
            {
                // Validate the token and return the ClaimsPrincipal (user claims)
                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch (Exception ex)
            {
                // If validation fails, throw an exception with an appropriate message
                throw new UnauthorizedAccessException("Invalid or expired token", ex);
            }
        }
        #endregion
    }
}