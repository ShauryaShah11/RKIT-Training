using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace FilterPractice.Filters
{
    public class CustomAuthorizeAttribute : Attribute, IFilterFactory
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

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

        public bool IsReusable => false;
    }
}