using System;
using System.Web;
using System.Web.Caching;

namespace WebAPIFinalDemo.Services
{
    /// <summary>
    /// Provides a simple wrapper around the ASP.NET Cache object for storing, 
    /// retrieving, and removing items in the cache with specified expiration policies.
    /// </summary>
    public class CacheService
    {
        // The underlying ASP.NET Cache object.
        private readonly Cache _cache = HttpContext.Current.Cache;

        /// <summary>
        /// Retrieves a cached item by its key.
        /// </summary>
        /// <typeparam name="T">The type of the item to retrieve.</typeparam>
        /// <param name="key">The unique key of the cached item.</param>
        /// <returns>The cached item if it exists; otherwise, the default value of type T.</returns>
        public T Get<T>(string key)
        {
            var cachedItem = _cache.Get(key);
            return cachedItem != null ? (T)cachedItem : default;
        }

        /// <summary>
        /// Adds an item to the cache with a specified expiration duration.
        /// </summary>
        /// <typeparam name="T">The type of the item to cache.</typeparam>
        /// <param name="key">The unique key for the cached item.</param>
        /// <param name="value">The item to store in the cache.</param>
        /// <param name="durationInSeconds">The time (in seconds) after which the item should expire.</param>
        public void Set<T>(string key, T value, int durationInSeconds)
        {
            _cache.Insert(
                key,                          // The unique key for the cached item
                value,                        // The item to store in the cache
                null,                         // No dependency (e.g., file or database dependency)
                DateTime.Now.AddSeconds(durationInSeconds), // Absolute expiration time
                Cache.NoSlidingExpiration     // Do not reset expiration on access
             );
        }

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="key">The unique key of the cached item to remove.</param>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}