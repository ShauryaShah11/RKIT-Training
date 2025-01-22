using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024; // Maximum cacheable size in bytes
    options.UseCaseSensitivePaths = true; // URLs are case-sensitive
});

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default30",
        new CacheProfile
        {
            Duration = 30, // Cache duration in seconds
            Location = ResponseCacheLocation.Any
        });
});

builder.Services.AddRequestDecompression();

#region Rate Limiting Middleware
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Rate limiter middleware should come early to prevent excessive requests
app.UseRateLimiter();

// Response caching middleware must come before authentication and other processing
app.UseResponseCaching();

app.UseStaticFiles(); // Correctly configure static file serving

app.UseResponseCompression();


app.Use(async (context, next) =>
{
    // Set Cache-Control headers only if not already set
    if (!context.Response.Headers.ContainsKey("Cache-Control"))
    {
        context.Response.GetTypedHeaders().CacheControl =
            new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(30)
            };

        context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
            new[] { "Accept-Encoding" };
    }

    await next();
});

app.MapControllers();

app.Run();
