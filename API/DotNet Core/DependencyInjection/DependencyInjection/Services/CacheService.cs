using DependencyInjection.Interfaces;

namespace DependencyInjection.Services
{
    /// <summary>
    /// Provides an in-memory caching mechanism.
    /// </summary>
    public class CacheService : ICacheService
    {
        /// <summary>
        /// Stores cached data using a dictionary.
        /// </summary>
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

        /// <summary>
        /// Retrieves cached data based on the provided cache key.
        /// </summary>
        /// <param name="cacheKey">The key used to retrieve cached data.</param>
        /// <returns>Returns the cached data if found, otherwise returns null.</returns>
        public string GetCacheedData(string cacheKey)
        {
            _cache.TryGetValue(cacheKey, out string data);
            return data;
        }

        //public string GetCacheedData(string cacheKey)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Stores data in the cache with a specified key.
        /// </summary>
        /// <param name="key">The key to store the data.</param>
        /// <param name="data">The data to be stored in the cache.</param>
        public void SetCachedData(string key, string data)
        {
            _cache[key] = data;
        }
    }
}
