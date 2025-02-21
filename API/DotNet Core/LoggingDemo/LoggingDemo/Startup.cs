namespace LoggingDemo
{
    /// <summary>
    /// Configures the application services and request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Holds the application configuration settings.
        /// </summary>
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
        /// <param name="services">The service collection to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            /// <summary>
            /// Adds controller services to the application.
            /// </summary>
            services.AddControllers();

            /// <summary>
            /// Adds API endpoint exploration capabilities for OpenAPI documentation.
            /// </summary>
            services.AddEndpointsApiExplorer();

            /// <summary>
            /// Registers Swagger to generate API documentation.
            /// </summary>
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the application's request pipeline.
        /// </summary>
        /// <param name="app">The application instance.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            /// <summary>
            /// Configures Swagger UI and API documentation in development mode.
            /// </summary>
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /// <summary>
            /// Enforces HTTPS redirection for secure communication.
            /// </summary>
            app.UseHttpsRedirection();

            /// <summary>
            /// Enables authorization middleware.
            /// </summary>
            app.UseAuthorization();

            /// <summary>
            /// Maps controllers to handle incoming HTTP requests.
            /// </summary>
            app.MapControllers();
        }
    }
}
