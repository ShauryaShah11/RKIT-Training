using FinalDemo.Middlewares;
using System.Globalization;

namespace FinalDemo.Middlewares
{
    /// <summary>
    /// Middleware to set the request culture based on a query parameter.
    /// </summary>
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCultureMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware logic to set the culture.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the completion of the middleware execution.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            string cultureQuery = context.Request.Query["culture"];
            if (!String.IsNullOrWhiteSpace(cultureQuery))
            {
                CultureInfo? culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            await _next(context);
        }
    }
}

/// <summary>
/// Extension methods for adding the <see cref="RequestCultureMiddleware"/> to the application pipeline.
/// </summary>
public static class RequestCultureMiddlewareExtensions
{
    /// <summary>
    /// Adds the <see cref="RequestCultureMiddleware"/> to the application's request pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The application builder with the middleware added.</returns>
    public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCultureMiddleware>();
    }
}
