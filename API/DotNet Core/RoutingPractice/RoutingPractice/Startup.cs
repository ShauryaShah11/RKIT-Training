using RoutingPractice.Models;

public class Startup
{
    /// <summary>
    /// Gets the configuration settings.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration settings.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="env">The hosting environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapWhen(
            context => context.Request.Path.StartsWithSegments("/admin"),
            branch => branch.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Admin Area");
                await next();
            })
        );

        app.MapWhen(
            context => context.Request.Query.ContainsKey("special"),
            branch => branch.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Special Query parameter detected");
                await next();
            })
        );

        app.MapWhen(
            context => context.Request.Headers.ContainsKey("x-custom-header"),
            branch => branch.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Special Header detected");
                await next();
            })
        );

        app.MapWhen(
            context => context.Request.Host.Host == "admin.example.com",
            branch => branch.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Admin Host");
                await next();
            })
        );

        app.MapWhen(
            context => context.Request.Path.StartsWithSegments("/api") && context.Request.Method == "POST",
            branch => branch.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Post Request to API");
                await next();
            })
        );

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapGet("/", () => "Hello, World!");

            endpoints.MapGet("/products/{id:int}", (int id) =>
                Results.Ok(new Product { Id = id, Name = $"Product {id}" }));

            // Map route with default id value
            endpoints.MapGet("/products/default/{id=1}", (int id) =>
            {
                // Access the id value here
                return $"Product ID: {id}";
            });

            endpoints.MapGet("/products/optional/{id?}", (int id) =>
            {
                return $"Product Id: {id}";
            });

            endpoints.MapPost("/products", (Product product) =>
                Results.Created($"/products/{product.Id}", product));

            endpoints.MapPut("/products/{id:int}", (int id, Product product) =>
            {
                product.Id = id;
                return Results.Ok(product);
            });

            endpoints.MapDelete("/products/{id:int}", (int id) =>
                Results.Ok($"Product with ID {id} has been deleted"));
        });
    }
}