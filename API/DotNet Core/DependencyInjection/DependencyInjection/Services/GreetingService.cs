using DependencyInjection.Interfaces;

namespace DependencyInjection.Services
{
    public class GreetingService: IGreetingService
    {
        public string GetGreeting(string name)
        {
            return $"Hello {name}";
        }
    }
}
