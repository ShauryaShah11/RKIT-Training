namespace ServiceLifecycleDemo
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method where execution starts.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            /// <summary>
            /// Creates a WebApplication builder with configuration settings.
            /// </summary>
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Initializes the Startup class with configuration.
            /// </summary>
            var startup = new Startup(builder.Configuration);

            /// <summary>
            /// Registers services into the dependency injection container.
            /// </summary>
            startup.ConfigureServices(builder.Services);

            /// <summary>
            /// Builds the application.
            /// </summary>
            var app = builder.Build();

            /// <summary>
            /// Configures the middleware pipeline.
            /// </summary>
            startup.Configure(app, app.Environment);

            /// <summary>
            /// Runs the application.
            /// </summary>
            app.Run();
        }
    }
}
