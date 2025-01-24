using DependencyInjection.Interfaces;

namespace DependencyInjection.Services
{
    public class LoggingService : ILoggingService
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}
