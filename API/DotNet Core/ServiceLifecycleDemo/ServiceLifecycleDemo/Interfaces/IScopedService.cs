namespace ServiceLifecycleDemo.Interfaces
{
    /// <summary>
    /// Represents a service with a Scoped lifetime.
    /// A new instance is created for each request within the same scope.
    /// </summary>
    public interface IScopedService
    {
        /// <summary>
        /// Retrieves a unique identifier (GUID) for the service instance.
        /// </summary>
        /// <returns>A string representing the unique GUID.</returns>
        string GetGuid();
    }
}
