namespace MiddlewarePractice.Middlewares
{
    /// <summary>
    /// Custom middleware that writes a message to the response.
    /// </summary>
    public class MyMiddleware : IMiddleware
    {
        /// <summary>
        /// Invokes the middleware logic.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <returns>A task that represents the completion of the middleware execution.</returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware Started");
            await next(context);
            //await context.Response.WriteAsync("Custom Middleware Ended");
        }
    }
}