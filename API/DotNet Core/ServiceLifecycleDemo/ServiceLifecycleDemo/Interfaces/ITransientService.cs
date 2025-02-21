namespace ServiceLifecycleDemo.Interfaces
{
    /// <summary>
    /// Represents a service with a Transient lifetime.
    /// A new instance is created each time it is requested.
    /// </summary>
    public interface ITransientService
    {
        /// <summary>
        /// Retrieves a unique identifier (GUID) for the service instance.
        /// </summary>
        /// <returns>A string representing the unique GUID.</returns>
        string GetGuid();
    }
}
