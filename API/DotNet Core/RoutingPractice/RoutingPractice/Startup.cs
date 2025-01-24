using RoutingPractice.Models;
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

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
