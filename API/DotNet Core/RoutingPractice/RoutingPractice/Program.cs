var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Demonstrate GET method using MapGet
app.MapGet("/", () => "Hello, World!");

// Demonstrate GET method for a specific route
app.MapGet("/products/{id:int}", (int id) => $"Product with ID: {id}");

app.MapPost("/products", (string product) =>
{
    // Simulate product creation
    return Results.Created($"/products/{1}", product);  // ID is simulated as 1
});

app.MapPut("/products/{id:int}", (int id, string product) =>
{
    return $"Product with ID {id} has been updated to {product}";
});

app.MapDelete("/products/{id:int}", (int id) =>
{
    return $"Product with ID {id} has been deleted";
});

app.MapWhen(
    context => context.Request.Path.StartsWithSegments("/admin"),
    branch =>
    {
        branch.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Admin Area");
            await next();
        });
    }
);

app.MapWhen(
    context => context.Request.Query.ContainsKey("special"),
    branch =>
    {
        branch.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Speacial Query parameter detected");
            await next();
        });
    }
);

app.MapWhen(
    context => context.Request.Headers.ContainsKey("x-custom-header"),
    branch =>
    {
        branch.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Special Header detected");
            await next();
        }); 
    }
);

app.MapWhen(
    context => context.Request.Host.Host == "admin.example.com",
    branch =>
    {
        branch.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Admin Host");
            await next();
        });
    }
);

app.MapWhen(
    context => context.Request.Path.StartsWithSegments("/api") && context.Request.Method == "POST",
    branch =>
    {
        branch.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Post Request to API");
            await next();
        });
    }
);

app.Run();
