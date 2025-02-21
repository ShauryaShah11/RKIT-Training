using DependencyInjection.Interfaces;
using System;

namespace DependencyInjection.Services
{
    /// <summary>
    /// Service that provides logging functionality.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}
