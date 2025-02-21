using ServiceLifecycleDemo.Interfaces;

namespace ServiceLifecycleDemo.Services
{
    /// <summary>
    /// Represents a service with a Singleton lifetime.
    /// </summary>
    public class SingletonService : ISingletonService
    {
        private readonly string _guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonService"/> class.
        /// Generates a unique GUID that remains the same throughout the application's lifetime.
        /// </summary>
        public SingletonService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Retrieves the unique identifier (GUID) of the singleton service instance.
        /// </summary>
        /// <returns>A string representing the GUID.</returns>
        public string GetGuid()
        {
            return _guid;
        }
    }
}
