using NLog;
using NLog.Web;

namespace FinalDemo
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that starts the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            // Create a new WebApplication builder.
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Setup NLog for logging.
            /// Loads configuration from "nlog.config".
            /// </summary>
            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
            logger.Info("Application is starting...");

            // <summary>
            /// Configure Logging settings.
            /// </summary>
            builder.Logging.ClearProviders(); /// <remarks>Removes default logging providers (e.g., Console, Debug).</remarks>
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); /// <remarks>Sets the minimum logging level to capture detailed logs.</remarks>
            builder.Host.UseNLog(); /// <remarks>Integrates NLog as the logging provider for the application.</remarks>

            // Instantiate the Startup class with configuration settings.
            var startup = new Startup(builder.Configuration);

            // Configure services required for the application.
            startup.ConfigureServices(builder.Services);

            // Build the application.
            var app = builder.Build();

            // Configure the middleware and HTTP request pipeline.
            startup.Configure(app, app.Environment);

            // Run the application.
            app.Run();
        }
    }
}
