using DependencyInjection.Interfaces;

namespace DependencyInjection.Services
{
    /// <summary>
    /// Service that provides greeting messages.
    /// </summary>
    public class GreetingService : IGreetingService
    {
        /// <summary>
        /// Returns a greeting message with the provided name.
        /// </summary>
        /// <param name="name">The name to include in the greeting.</param>
        /// <returns>A greeting message.</returns>
        public string GetGreeting(string name)
        {
            return $"Hello {name}";
        }
    }
}
