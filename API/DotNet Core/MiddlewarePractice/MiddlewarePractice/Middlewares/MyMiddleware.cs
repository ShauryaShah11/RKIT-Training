namespace MiddlewarePractice.Middlewares
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware Started");
            await next(context);
            //await context.Response.WriteAsync("Custom Middleware Ended");
        }
    }
}
