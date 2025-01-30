using FinalDemo.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Attributes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles) : base(typeof(AuthorizationFilter))
        {
            // Pass the roles into the filter constructor
            Arguments = new object[] { roles };
        }
    }
}
