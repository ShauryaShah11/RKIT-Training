using ServiceLifecycleDemo.Interfaces;

namespace ServiceLifecycleDemo.Services
{
    public class ScopedService : IScopedService
    {
        private readonly string _guid;
        public ScopedService()
        {
            _guid = Guid.NewGuid().ToString();
        }
        public string GetGuid()
        {
            return _guid;
        }
    }
}
