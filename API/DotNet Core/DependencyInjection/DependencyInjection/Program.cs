namespace DependencyInjection
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method where the application starts execution.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            /// <summary>
            /// Creates a new instance of WebApplicationBuilder to configure the application.
            /// </summary>
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Initializes the Startup class to configure services and middleware.
            /// </summary>
            var startup = new Startup(builder.Configuration);

            /// <summary>
            /// Configures services for dependency injection.
            /// </summary>
            startup.ConfigureServices(builder.Services);

            /// <summary>
            /// Builds the application pipeline.
            /// </summary>
            var app = builder.Build();

            /// <summary>
            /// Configures the application request pipeline.
            /// </summary>
            startup.Configure(app, app.Environment);

            /// <summary>
            /// Runs the application.
            /// </summary>
            app.Run();
        }
    }
}
