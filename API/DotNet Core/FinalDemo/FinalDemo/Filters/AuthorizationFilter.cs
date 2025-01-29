using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalDemo.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly string[] _roles;
        public AuthorizationFilter(IConfiguration configuration, string[] roles = null)
        {
            _configuration = configuration;
            _roles = roles ?? Array.Empty<string>();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            // Ensure the token is present and properly formatted
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string? token = authorizationHeader.Substring("Bearer ".Length).Trim();

            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

                // Validate the token using standard JWT settings
                TokenValidationParameters parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:issuer"],
                    ValidAudience = _configuration["Jwt:audience"],
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
