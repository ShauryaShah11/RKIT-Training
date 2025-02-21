using DependencyInjection.Interfaces;
using DependencyInjection.Services;
using System.Net.Security;

namespace DependencyInjection
{
    /// <summary>
    /// Configures application services and middleware.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Represents the application configuration settings.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration settings for the application.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures services and registers them with the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Registers controllers
            services.AddControllers();

            // Registers a scoped service (created once per request)
            services.AddScoped<IGreetingService, GreetingService>();

            // Registers a singleton service (created once for the lifetime of the application)
            services.AddSingleton<ICacheService, CacheService>();

            // Registers a transient service (created every time it is requested)
            services.AddTransient<ILoggingService, LoggingService>();

            // Adds support for API exploration and documentation (Swagger)
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the middleware pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            // Enables the Developer Exception Page in development mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enables Swagger UI for API documentation
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enforces HTTPS redirection
            app.UseHttpsRedirection();

            // Enables authorization middleware
            app.UseAuthorization();

            // Maps controllers to handle incoming API requests
            app.MapControllers();
        }
    }
}
