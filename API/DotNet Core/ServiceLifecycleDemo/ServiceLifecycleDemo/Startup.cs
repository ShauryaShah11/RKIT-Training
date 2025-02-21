using ServiceLifecycleDemo.ExtensionMethods;

namespace ServiceLifecycleDemo
{
    /// <summary>
    /// Configures services and middleware for the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration settings.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures services and adds them to the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to register dependencies.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            /// <summary>
            /// Adds MVC controllers to the service collection.
            /// </summary>
            services.AddControllers();

            /// <summary>
            /// Registers Swagger for API documentation.
            /// </summary>
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            /// <summary>
            /// Registers application-specific services via an extension method.
            /// </summary>
            services.AddApplicationServices();
        }

        /// <summary>
        /// Configures the HTTP request pipeline and middleware.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            /// <summary>
            /// Enables developer exception page and Swagger UI in development mode.
            /// </summary>
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /// <summary>
            /// Configures a global exception handler for non-development environments.
            /// </summary>
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }

            /// <summary>
            /// Enforces HTTPS redirection.
            /// </summary>
            app.UseHttpsRedirection();

            /// <summary>
            /// Enables authorization middleware.
            /// </summary>
            app.UseAuthorization();

            /// <summary>
            /// Maps controller endpoints to handle HTTP requests.
            /// </summary>
            app.MapControllers();
        }
    }
}
