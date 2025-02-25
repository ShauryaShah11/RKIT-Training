using FinalDemo.ExtensionMethods;
using FinalDemo.Filters;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Text;
using System.Text.Json.Serialization;

namespace FinalDemo
{
    /// <summary>
    /// Configures application services and middleware.
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
        /// Configures services required by the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure JSON serialization settings
            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        Dictionary<string, string[]>? errors = context.ModelState
                            .Where(x => x.Value?.Errors.Count > 0)
                            .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                            );

                        var result = new
                        {
                            Status = "Error",
                            Message = "Validation failed",
                            Errors = errors
                        };

                        return new BadRequestObjectResult(result);
                    };
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Ignore null values
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; // Pretty-print JSON
                    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Prevent circular references
                    //options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(); // Use camelCase
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; // Custom date format
                    //options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    //{
                    //    IgnoreSerializableAttribute = true // Ignore [Serializable] attributes
                    //};
                });

            // Register HTTP Context Accessor
            services.AddHttpContextAccessor();

            // Enable API documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configure JWT Authentication
            string jwtSecretKey = _configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("JWT Secret Key is not configured");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
                };
            });

            // Configure Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            });

            // Register OrmLite Database Factory
            services.AddSingleton<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(
                    _configuration.GetConnectionString("DefaultConnection"),
                    MySqlDialect.Provider
                )
            );

            // Configure JSON serialization options
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            // Register application services
            services.AddApplicationServices(_configuration);
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Ensure database tables exist
            using (var scope = app.Services.CreateScope())
            {
                var dbFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
                using (var db = dbFactory.Open())
                {
                    db.CreateTableIfNotExists<YMU01>(); // User table
                    db.CreateTableIfNotExists<YMS01>(); // Stock table
                    db.CreateTableIfNotExists<YMH01>(); // Stock history table
                    db.CreateTableIfNotExists<YMO01>(); // Orders table
                }
            }
        }
    }
}
