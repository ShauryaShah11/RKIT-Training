using System;
using System.IO;

namespace LibraryManagement.Logging
{
    /// <summary>
    /// The Logger class is responsible for logging messages to a log file.
    /// </summary>
    public static class Logger
    {
        // The log file path (can be customized to your project directory)
        private static readonly string logFilePath = "LibraryLog.txt";

        /// <summary>
        /// Writes a log entry to the log file.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Log(string message)
        {
            try
            {
                // Add timestamp to the message
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

                // Write the message to the log file (append if exists)
                using (StreamWriter sw = new StreamWriter(logFilePath, append: true))
                {
                    sw.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle any logging error (you can log this exception as well)
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }
    }
}
