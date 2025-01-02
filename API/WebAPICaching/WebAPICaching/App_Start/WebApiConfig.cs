using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;
using System.Web.Http;

namespace WebAPICaching
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing (essential for versioned routes)
            config.MapHttpAttributeRoutes();

            // URI-based versioning route (version in the URL path)
            config.Routes.MapHttpRoute(
                name: "VersionedApi",
                routeTemplate: "api/v{version:apiVersion}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Default route (fallback) - For general routes without versioning in the URL
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add API versioning with multiple readers for different versioning strategies
            config.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0); // Default version is 1.0
                options.ReportApiVersions = true;

                // Combine multiple version readers
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),  // Reads version from the URI path (e.g., /api/v1/)
                    new HeaderApiVersionReader("X-API-Version"), // Reads version from header
                    new QueryStringApiVersionReader("version"), // Reads version from query string
                    new MediaTypeApiVersionReader("api-version") // Reads version from media type
                );
            });
        }
    }
}
