using Asp.Versioning;
using Swashbuckle.Application;
using System.Web.Http;

namespace WebAPIFinalDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing (essential for versioned routes)
            config.MapHttpAttributeRoutes();

            // Register API versioning with multiple strategies
            config.AddApiVersioning(options =>
            {
                // Versioning by URL Path (e.g., /api/v1/tasks)
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-API-Version"),
                    new MediaTypeApiVersionReader("vnd.myapi")
                );

                // Default version if no versioning is specified
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                // Report API versions to the API explorer
                options.ReportApiVersions = true;
            });

            // Versioned route (URL path versioning)
            config.Routes.MapHttpRoute(
                name: "ApiVersionedByPath",
                routeTemplate: "api/v{version:apiVersion}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Default route (fallback) for non-versioned APIs
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Swagger Redirect Route for Swagger UI configuration
            config.Routes.MapHttpRoute(
                name: "SwaggerRedirect",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(message => message.RequestUri.ToString(), "swagger")
            );

        }
    }
}
