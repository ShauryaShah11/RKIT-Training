using Swashbuckle.Application;
using System.Web.Http;
using WebAPISecurity.Filters;

namespace WebAPISecurity
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS for cross-origin requests
            config.EnableCors();

            // Enable attribute routing (allows you to use [Route] and [HttpGet] etc. on actions)
            config.MapHttpAttributeRoutes();

            // Web API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Swagger Redirect Route (Swagger UI configuration)
            config.Routes.MapHttpRoute(
                name: "SwaggerRedirect",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(message => message.RequestUri.ToString(), "swagger")
            );

            // Optionally, you can add a global exception filter to handle exceptions globally
            config.Filters.Add(new GlobalExceptionFilter());
        }
    }
}
