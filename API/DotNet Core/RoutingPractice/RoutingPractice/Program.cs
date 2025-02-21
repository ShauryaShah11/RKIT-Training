namespace RoutingPractice
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method which is the entry point of the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Call Startup methods
            var startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            // Configure the middleware pipeline
            startup.Configure(app, app.Environment);

            app.Run();
        }
    }
}