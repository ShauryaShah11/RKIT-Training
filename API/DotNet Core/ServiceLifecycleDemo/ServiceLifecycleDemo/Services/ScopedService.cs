using ServiceLifecycleDemo.Interfaces;

namespace ServiceLifecycleDemo.Services
{
    /// <summary>
    /// Represents a service with a Scoped lifetime.
    /// </summary>
    public class ScopedService : IScopedService
    {
        private readonly string _guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedService"/> class.
        /// Generates a new GUID for the service instance.
        /// </summary>
        public ScopedService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Retrieves the unique identifier (GUID) of the service instance.
        /// </summary>
        /// <returns>A string representing the GUID.</returns>
        public string GetGuid()
        {
            return _guid;
        }
    }
}
