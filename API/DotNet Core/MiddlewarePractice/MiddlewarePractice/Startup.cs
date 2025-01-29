using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiddlewarePractice.Middlewares;
using System.Text;
using System.Threading.RateLimiting;

namespace MiddlewarePractice
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<MyMiddleware>();

            // Register services
            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024; // Maximum cacheable size in bytes
                options.UseCaseSensitivePaths = true; // URLs are case-sensitive
            });

            services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default30",
                    new CacheProfile
                    {
                        Duration = 30, // Cache duration in seconds
                        Location = ResponseCacheLocation.Any
                    });
            });

            services.AddRequestDecompression();

            services.AddResponseCompression();

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

            string jwtSecretKey = _configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("JWT Secret Key is not configured");

            // Configure JWT Authentication
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
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecretKey))
                };
            });

            // Configure Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Admin"));
            });
        }

        public void Configure(WebApplication app, IHostEnvironment env)
        {
            // Developer exception page middleware should be first in development
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // HTTPS redirection
            app.UseHttpsRedirection();

            // Static files middleware
            app.UseStaticFiles();

            // Enable response caching (added)
            //app.UseResponseCaching();

            // Routing middleware before authentication and authorization
            app.UseRouting();

            // Move the 'UseWhen' conditional middleware before routing
            // So, it executes before other middleware like Authentication/Authorization
            app.UseWhen(
                context => context.Request.Query.ContainsKey("IsAuthorized") &&
                           context.Request.Query["IsAuthorized"] == "true",
                branch =>
                {
                    branch.Use(async (context, next) =>
                    {
                        if (!context.Response.HasStarted)
                        {
                            await context.Response.WriteAsync("Shaurya here");
                        }
                        await next(context);
                    });
                }
            );            

            // Authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers (final step)
            app.MapControllers();

            app.Run();
        }

    }
}
