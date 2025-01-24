using DependencyInjection.Interfaces;

namespace DependencyInjection.Services
{
    public class CacheService: ICacheService
    {
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

        public string GetCacheedData(string cacheKey)
        {
            _cache.TryGetValue(cacheKey, out string data);
            return data;
        }

        public void SetCachedData(string key, string data)
        {
            _cache[key] = data;
        }
    }
}
