using ServiceLifecycleDemo.Interfaces;

namespace ServiceLifecycleDemo.Services
{
    /// <summary>
    /// Represents a service with a Transient lifetime.
    /// A new instance of this service is created every time it is requested.
    /// </summary>
    public class TransientService : ITransientService
    {
        private readonly string _guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransientService"/> class.
        /// Generates a unique GUID for each instance of the service.
        /// </summary>
        public TransientService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Retrieves the unique identifier (GUID) of the transient service instance.
        /// </summary>
        /// <returns>A string representing the GUID.</returns>
        public string GetGuid()
        {
            return _guid;
        }
    }
}
