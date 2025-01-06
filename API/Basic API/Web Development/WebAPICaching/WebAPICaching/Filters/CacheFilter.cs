using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace WebAPICaching.Filters
{
    /// <summary>
    /// The CacheFilter class is a custom action filter that adds caching headers
    /// to the HTTP response to enable caching for the specified duration.
    /// It sets the Cache-Control header with the maximum age and revalidation options.
    /// </summary>
    public class CacheFilter : ActionFilterAttribute
    {
        #region Public Members
        /// <summary>
        /// Gets or sets the duration (in seconds) for which the response should be cached.
        /// </summary>
        public int TimeDuration { get; set; }
        #endregion

        #region Public Methods

        /// <summary>
        /// Executes after the action method has been executed, adding caching headers 
        /// to the HTTP response.
        /// </summary>
        /// <param name="actionExecutedContext">The context of the executed action.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(TimeDuration),
                MustRevalidate = true,
                Public = true
            };
        }
        #endregion
    }
}
