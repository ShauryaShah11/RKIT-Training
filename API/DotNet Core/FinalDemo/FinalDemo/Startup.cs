using FinalDemo.ExtensionMethods;
using FinalDemo.Filters;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Text;
using System.Text.Json.Serialization;

namespace FinalDemo
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
            // Add services to the container.
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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

            // Register OrmLite Database Factory
            services.AddSingleton<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(_configuration.GetConnectionString("DefaultConnection"),
                MySqlDialect.Provider)
            );

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    // Or to ignore cycles
                    // options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            services.AddApplicationServices(_configuration);
        }

        public void Configure(WebApplication app, IHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var dbFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();

                using (var db = dbFactory.Open())
                {
                    db.CreateTableIfNotExists<YMU01>();
                    db.CreateTableIfNotExists<YMS01>();
                    db.CreateTableIfNotExists<YMH01>();
                    db.CreateTableIfNotExists<YMO01>();
                }
            }
        }
    }
}
