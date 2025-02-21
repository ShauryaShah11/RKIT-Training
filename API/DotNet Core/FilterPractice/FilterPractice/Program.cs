namespace FilterPractice
{
    public class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            /// <summary>
            /// Creates a new WebApplication builder.
            /// </summary>
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Initializes the Startup class and passes configuration settings.
            /// </summary>
            var startup = new Startup(builder.Configuration);

            /// <summary>
            /// Configures services such as dependency injection and middleware.
            /// </summary>
            startup.ConfigureServices(builder.Services);

            /// <summary>
            /// Builds the application.
            /// </summary>
            var app = builder.Build();

            /// <summary>
            /// Configures the middleware pipeline, including routing and authentication.
            /// </summary>
            startup.Configure(app, app.Environment);

            /// <summary>
            /// Runs the application.
            /// </summary>
            app.Run();
        }
    }
}
