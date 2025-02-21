using FilterPractice.Filters;
using Microsoft.OpenApi.Models;

namespace FilterPractice
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures services for dependency injection and middleware.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            /// <summary>
            /// Adds controllers to the application.
            /// </summary>
            services.AddControllers();

            /// <summary>
            /// Registers global filters for handling exceptions, resources, actions, and results.
            /// </summary>
            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
                options.Filters.Add<CustomResourceFilter>();
                options.Filters.Add<CustomActionFilter>();
                options.Filters.Add<CustomResultFilter>();
            });

            /// <summary>
            /// Configures Swagger for API documentation.
            /// </summary>
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                /// <summary>
                /// Defines the JWT authentication scheme for Swagger UI.
                /// </summary>
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token."
                });

                /// <summary>
                /// Adds a security requirement to enforce authentication in Swagger UI.
                /// </summary>
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        /// <summary>
        /// Configures the middleware pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            /// <summary>
            /// Enables Swagger UI only in the development environment.
            /// </summary>
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /// <summary>
            /// Enforces HTTPS redirection.
            /// </summary>
            app.UseHttpsRedirection();

            /// <summary>
            /// Maps controller routes.
            /// </summary>
            app.MapControllers();
        }
    }
}
