namespace Controller_Action_Practice
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
            // Create a new instance of WebApplication builder
            var builder = WebApplication.CreateBuilder(args);

            // Create an instance of Startup to configure services and middleware
            var startup = new Startup(builder.Configuration);

            // Configure services
            startup.ConfigureServices(builder.Services);

            // Build the application
            var app = builder.Build();

            // Configure the middleware pipeline
            startup.Configure(app, app.Environment);

            // Run the application
            app.Run();
        }
    }
}
