using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;
using System.Web.Http;

namespace WebAPIVersioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Versioned Routes for Version 1 and Version 2
            config.Routes.MapHttpRoute(
                name: "Version1",
                routeTemplate: "api/v1/students/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "StudentV1" }
            );

            config.Routes.MapHttpRoute(
                name: "Version2",
                routeTemplate: "api/v2/students/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "StudentV2" }
            );

            // Add API versioning
            config.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0); // Default version is 1.0
                options.ReportApiVersions = true;

                // Configure query string-based versioning
                options.ApiVersionReader = new QueryStringApiVersionReader("version");

                // Optional: Can add other version readers here if needed (e.g., Header, URL Segment)
            });

            // Optional: If you want to explicitly handle routes that use versioning
            // config.Routes.MapHttpRoute(
            //     name: "VersionedRoute",
            //     routeTemplate: "api/{version:apiVersion}/students/{id}",
            //     defaults: new { id = RouteParameter.Optional, controller = "Student" }
            // );
        }
    }
}
