using FinalDemo.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Attributes
{
    /// <summary>
    /// Custom attribute to authorize access based on specific roles.
    /// This attribute utilizes a custom authorization filter to check user roles before granting access to the associated action.
    /// </summary>
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Constructor to initialize the CustomAuthorize attribute with roles.
        /// The provided roles are passed into the authorization filter to control access.
        /// </summary>
        /// <param name="roles">Array of roles that are authorized to access the decorated action.</param>
        public CustomAuthorizeAttribute(params string[] roles) : base(typeof(AuthorizationFilter))
        {
            // Pass the roles into the filter constructor
            Arguments = new object[] { roles };
        }
    }
}