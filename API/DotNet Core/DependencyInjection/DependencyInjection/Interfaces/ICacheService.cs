namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Defines a contract for caching services.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Retrieves cached data based on the provided cache key.
        /// </summary>
        /// <param name="cacheKey">The key associated with the cached data.</param>
        /// <returns>The cached data if found; otherwise, null.</returns>
        string GetCacheedData(string cacheKey);

        /// <summary>
        /// Stores data in the cache with a specified key.
        /// </summary>
        /// <param name="key">The key to store the data under.</param>
        /// <param name="data">The data to be cached.</param>
        void SetCachedData(string key, string data);
    }
}
