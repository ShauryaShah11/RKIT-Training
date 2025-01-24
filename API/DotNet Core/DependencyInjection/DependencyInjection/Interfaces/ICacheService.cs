namespace DependencyInjection.Interfaces
{
    public interface ICacheService
    {
        string GetCacheedData(string cacheKey);
        void SetCachedData(string key, string data);
    }
}
