using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebAPISecurity.Services;

namespace WebAPISecurity.Attributes
{
    /// <summary>
    /// A custom authorization attribute for validating Bearer tokens in HTTP requests.
    /// </summary>
    public class BearerAuth : AuthorizeAttribute
    {
        /// <summary>
        /// Performs authorization by validating the Bearer token in the Authorization header.
        /// </summary>
        /// <param name="actionContext">The context of the current HTTP action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Skip authorization if the action or controller allows anonymous access
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrWhiteSpace(authHeader.Parameter))
            {
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            try
            {
                var token = authHeader.Parameter;
                var principal = TokenService.ValidateToken(token);

                // Set the authenticated principal
                Thread.CurrentPrincipal = principal;
                actionContext.RequestContext.Principal = principal;
            }
            catch (UnauthorizedAccessException)
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        /// <summary>
        /// Handles unauthorized requests by setting the response to HTTP 401 Unauthorized.
        /// </summary>
        /// <param name="actionContext">The context of the current HTTP action.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                new { message = "Unauthorized access" }
            );
        }
    }
}
