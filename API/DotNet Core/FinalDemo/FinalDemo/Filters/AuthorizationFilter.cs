using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalDemo.Filters
{
    /// <summary>
    /// Custom authorization filter that validates JWT tokens for incoming requests.
    /// It checks the token's validity, including issuer, audience, and signature.
    /// If roles are specified, the filter ensures the user has one of the required roles.
    /// If the validation fails, the request is unauthorized or forbidden.
    /// The claims from the token are added to the HttpContext for further use in the controller.
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly string[] _roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFilter"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration to access JWT settings.</param>
        /// <param name="roles">Optional array of roles to check against user claims.</param>
        public AuthorizationFilter(IConfiguration configuration, string[] roles = null)
        {
            _configuration = configuration;
            _roles = roles ?? Array.Empty<string>();
        }

        /// <summary>
        /// Executes the authorization logic for the incoming request.
        /// Validates the JWT token and checks user roles if provided.
        /// </summary>
        /// <param name="context">The context for the authorization request.</param>
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
