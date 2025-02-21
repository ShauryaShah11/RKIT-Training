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
