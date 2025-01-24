using MiddlewarePractice;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the middleware pipeline
startup.Configure(app, app.Environment);

app.Run();
