using FinalDemo.ExtensionMethods;
using FinalDemo.Filters;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Text;
using System.Threading.RateLimiting;

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
            // [SECURITY] A Cross-Origin Resource Sharing (CORS) policy
            // Controls which domains can access your API
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins(
                                _configuration.GetSection("CorsOrigins").Get<string[]>() ??
                                new[] { "https://localhost:3000" })
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // [PERFORMANCE] Enable response compression to reduce payload size
            // Compresses HTTP responses to improve network performance
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true; // Enable compression even for HTTPS
            });

            // [PERFORMANCE] A response caching to improve response times for repeated requests
            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024; // Size limit in bytes for cacheable responses
                options.UseCaseSensitivePaths = false; // Case insensitive path matching
            });


            // Configure JSON serialization settings
            services.AddControllers(options =>
            {
                // [ERROR HANDLING] A global exception filter
                options.Filters.Add<CustomExceptionFilter>();
            })
                .ConfigureApiBehaviorOptions(options =>
                {
                    // [VALIDATION] Custom model validation response
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
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; // Custom date format
                });

            // [CONTEXT ACCESS] Register HTTP Context Accessor
            // Makes HttpContext available via dependency injection
            services.AddHttpContextAccessor();

            // [DOCUMENTATION] Enable API documentation
            services.AddEndpointsApiExplorer();

            // [SECURITY] Rate Limiting Middleware
            // Protects against DoS attacks by limiting request frequency
            #region Rate Limiting Middleware
            services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 4;
                    options.Window = TimeSpan.FromSeconds(12);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 2;
                }));
            #endregion

            // [DOCUMENTATION] Configure Swagger/OpenAPI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FinalDemo API",
                    Version = "v1",
                    Description = "API for managing users, stocks, and orders",
                    Contact = new OpenApiContact
                    {
                        Name = "API Support Team",
                        Email = "support@example.com"
                    }
                });

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

                // A security requirement
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

            // [SECURITY] Configure JWT Authentication
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

            // [SECURITY] Configure Authorization Policies
            services.AddAuthorization();

            // [DATABASE] Register OrmLite Database Factory
            services.AddSingleton<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(
                    _configuration.GetConnectionString("DefaultConnection"),
                    MySqlDialect.Provider
                )
            );

            // [DI] Register application services
            services.AddApplicationServices(_configuration);
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(WebApplication app, IHostEnvironment env)
        {
            // [DEVELOPMENT] Configure development-specific middleware
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                // [PRODUCTION] Use production error handler
                app.UseExceptionHandler("/error");
                // [SECURITY] Enable HTTP Strict Transport Security
                app.UseHsts();
            }

            // [PERFORMANCE] Enable response compression
            app.UseResponseCompression();

            // [PERFORMANCE] Enable response caching
            app.UseResponseCaching();

            // [SECURITY] Redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // [STATIC FILES] Serve static files (CSS, JavaScript, images)
            app.UseStaticFiles(); // Fixed the duplicate app reference

            // [ROUTING] Enable endpoint routing
            app.UseRouting();

            app.UseRequestCulture();

            // [SECURITY] Enable CORS
            app.UseCors("DefaultCorsPolicy");

            // [SECURITY] Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // [SECURITY] Apply rate limiting
            app.UseRateLimiter();

            // [ROUTING] Map controllers and health checks
            app.MapControllers();

            // [DATABASE] Ensure database tables exist
            using (var scope = app.Services.CreateScope())
            {
                var dbFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
                using (var db = dbFactory.Open())
                {
                    // Create database tables if they don't exist
                    db.CreateTableIfNotExists<YMU01>(); // User table
                    db.CreateTableIfNotExists<YMS01>(); // Stock table
                    db.CreateTableIfNotExists<YMH01>(); // Stock history table
                    db.CreateTableIfNotExists<YMO01>(); // Orders table
                }
            }
        }
    }
}