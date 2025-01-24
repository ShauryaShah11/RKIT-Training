using FilterPractice.Filters;
using Microsoft.OpenApi.Models;

namespace FilterPractice
{    
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ConfigureServices is used to register services
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers
            services.AddControllers();

            // Register the custom filters globally
            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
                options.Filters.Add<CustomResourceFilter>();
                options.Filters.Add<CustomActionFilter>();
                options.Filters.Add<CustomResultFilter>();
            });

            // Configure Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // Define the security scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token."
                });

                // Add security requirement
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

        // Configure is used to set up the middleware pipeline
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            // Enable Swagger only in development
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Map controllers
            app.MapControllers();
        }
    }
}
