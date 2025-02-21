using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FilterPractice.Filters
{
    /// <summary>
    /// Custom authorization filter for validating JWT tokens and user roles.
    /// </summary>
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _jwtSecretKey;
        private readonly string _validIssuer;
        private readonly string _validAudience;
        private readonly string[] _roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthorizationFilter"/> class.
        /// </summary>
        /// <param name="jwtSecretKey">The secret key used for signing JWT tokens.</param>
        /// <param name="validIssuer">The valid issuer of the token.</param>
        /// <param name="validAudience">The valid audience of the token.</param>
        /// <param name="roles">Optional list of roles required for authorization.</param>
        public CustomAuthorizationFilter(string jwtSecretKey, string validIssuer, string validAudience, string[] roles = null)
        {
            _jwtSecretKey = jwtSecretKey;
            _validIssuer = validIssuer;
            _validAudience = validAudience;
            _roles = roles ?? Array.Empty<string>();
        }

        /// <summary>
        /// Performs JWT token validation and role-based authorization.
        /// </summary>
        /// <param name="context">The authorization filter context.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            // Ensure the token is present and properly formatted
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSecretKey);

                // Validate the token using standard JWT settings
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _validIssuer,
                    ValidAudience = _validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out _);

                // If roles are provided, check if the user has one of the roles
                if (_roles.Length > 0)
                {
                    var userRoles = principal.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToArray();

                    // If user doesn't have the required role, return forbidden
                    if (!_roles.Intersect(userRoles).Any())
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }

                // Store the claims in HttpContext for further use in controllers
                context.HttpContext.Items["UserClaims"] = principal.Claims;
            }
            catch
            {
                context.Result = new UnauthorizedResult();  // Invalid token
            }
        }
    }
}
