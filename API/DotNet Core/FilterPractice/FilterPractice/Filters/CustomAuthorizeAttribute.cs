using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    /// <summary>
    /// Custom authorization attribute that applies role-based authentication using JWT.
    /// </summary>
    public class CustomAuthorizeAttribute : Attribute, IFilterFactory
    {
        private readonly string[] _roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="roles">Optional list of roles required for authorization.</param>
        public CustomAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        /// <summary>
        /// Creates an instance of the <see cref="CustomAuthorizationFilter"/> with configured JWT parameters.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to retrieve dependencies.</param>
        /// <returns>An instance of <see cref="CustomAuthorizationFilter"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if JWT configuration settings are missing.
        /// </exception>
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService(typeof(IConfiguration)) as IConfiguration;

            var jwtSecretKey = configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("JWT Secret Key is not configured");
            var validIssuer = configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer is not configured");
            var validAudience = configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("JWT Audience is not configured");

            return new CustomAuthorizationFilter(
                jwtSecretKey,
                validIssuer,
                validAudience,
                _roles);
        }

        /// <summary>
        /// Gets a value indicating whether the filter can be reused.
        /// </summary>
        public bool IsReusable => false;
    }
}
