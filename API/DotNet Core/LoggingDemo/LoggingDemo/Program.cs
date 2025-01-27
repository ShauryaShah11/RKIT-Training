using LoggingDemo;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()         // Log to console (optional)
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Log to file with daily rolling logs
    .CreateLogger();

// Add Serilog to the logging pipeline (this should be done before adding other logging providers)
builder.Host.UseSerilog();

// Add logging services
builder.Logging.ClearProviders(); // Clear default providers (this will remove the built-in console and debug logging)
builder.Logging.AddConsole();    // Add Console logging (optional, since it's handled by Serilog)
builder.Logging.AddDebug();      // Add Debug logging (optional, since it's handled by Serilog)

builder.Logging.AddFilter("LoggingDemo", LogLevel.Debug); // Only log for this namespace

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
