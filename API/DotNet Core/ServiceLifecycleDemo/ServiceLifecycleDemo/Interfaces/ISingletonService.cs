namespace ServiceLifecycleDemo.Interfaces
{
    /// <summary>
    /// Represents a service with a Singleton lifetime.
    /// A single instance is created and shared across the entire application.
    /// </summary>
    public interface ISingletonService
    {
        /// <summary>
        /// Retrieves a unique identifier (GUID) for the service instance.
        /// </summary>
        /// <returns>A string representing the unique GUID.</returns>
        string GetGuid();
    }
}
