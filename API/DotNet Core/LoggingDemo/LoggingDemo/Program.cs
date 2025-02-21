using NLog;
using NLog.Web;

namespace LoggingDemo
{
    /// <summary>
    /// Starting point of application
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Setup NLog for logging.
            /// Loads configuration from "nlog.config".
            /// </summary>
            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
            logger.Info("Application is starting...");

            /// <summary>
            /// Configure Logging settings.
            /// </summary>
            builder.Logging.ClearProviders(); /// <remarks>Removes default logging providers (e.g., Console, Debug).</remarks>
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); /// <remarks>Sets the minimum logging level to capture detailed logs.</remarks>
            builder.Host.UseNLog(); /// <remarks>Integrates NLog as the logging provider for the application.</remarks>

            try
            {
                /// <summary>
                /// Create an instance of Startup and configure services.
                /// </summary>
                var startup = new Startup(builder.Configuration);
                startup.ConfigureServices(builder.Services);

                /// <summary>
                /// Build the application.
                /// </summary>
                var app = builder.Build();

                /// <summary>
                /// Configure the application pipeline.
                /// </summary>
                startup.Configure(app, app.Environment);

                /// <summary>
                /// Log that the application has started successfully.
                /// </summary>
                logger.Info("Application has started successfully.");

                /// <summary>
                /// Start the application and begin processing requests.
                /// </summary>
                app.Run();
            }
            catch (Exception ex)
            {
                /// <summary>
                /// Log any exceptions that cause the application to stop unexpectedly.
                /// </summary>
                logger.Error(ex, "Application encountered an exception and stopped.");
                throw;
            }
            finally
            {
                /// <summary>
                /// Log application shutdown and ensure all logs are flushed before exiting.
                /// </summary>
                logger.Info("Application has stopped");
                LogManager.Shutdown();
            }
        }
    }
}
