using Controller_Action_Practice;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

// Configure services
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the middleware pipeline
startup.Configure(app, app.Environment);

app.Run();
