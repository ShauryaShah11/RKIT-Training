namespace Controller_Action_Practice
{
    /// <summary>
    /// Configures the services and middleware for the application.
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
        /// Registers services for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers to handle API requests
            services.AddControllers();

            // Enable API endpoint exploration
            services.AddEndpointsApiExplorer();

            // Add Swagger for API documentation
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the application's middleware pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            // Enable Swagger UI only in development mode
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enforce HTTPS for security
            app.UseHttpsRedirection();

            // Enable authorization middleware
            app.UseAuthorization();

            // Map controller endpoints
            app.MapControllers();
        }
    }
}
