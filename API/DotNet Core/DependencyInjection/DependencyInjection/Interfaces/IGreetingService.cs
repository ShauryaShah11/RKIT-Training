namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Defines a contract for a greeting service.
    /// </summary>
    public interface IGreetingService
    {
        /// <summary>
        /// Generates a greeting message for the specified name.
        /// </summary>
        /// <param name="name">The name to include in the greeting.</param>
        /// <returns>A greeting message.</returns>
        string GetGreeting(string name);
    }
}
