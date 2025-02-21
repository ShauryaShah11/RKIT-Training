namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Defines a contract for logging messages.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs a message to the console or a logging system.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Log(string message);
    }
}
