using DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetController : ControllerBase
    {
        private readonly IGreetingService _greetingService;
        private readonly ICacheService _cacheService;
        private readonly ILoggingService _loggingService;

        public GreetController(IGreetingService greetingService, ICacheService cacheService, ILoggingService loggingService)
        {
            _greetingService = greetingService;
            _cacheService = cacheService;
            _loggingService = loggingService;
        }

        // Endpoint to greet a user
        [HttpGet("/greet")]
        public IActionResult Get([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name parameter is required.");
            }

            string greetingMessage = _greetingService.GetGreeting(name);
            return Ok(new { Message = greetingMessage });
        }

        // Endpoint to fetch products with caching
        [HttpGet("/cached-data")]
        public IActionResult GetData()
        {
            string key = "product";
            _loggingService.Log("Attempting to fetch products");

            string cachedProduct = _cacheService.GetCacheedData(key);

            if (cachedProduct == null)
            {
                _loggingService.Log("Cache miss - Fetching products from database or API");

                // Simulating fetching product data (replace with real DB/API call)
                string products = "[{\"id\": 1, \"name\": \"Product1\", \"price\": 100}, {\"id\": 2, \"name\": \"Product2\", \"price\": 200}]";

                // Cache the product data
                _cacheService.SetCachedData(key, products); // Cache for 10 minutes

                return Ok(new { Message = "Products fetched and cached", Data = products });
            }

            _loggingService.Log("Cache hit - Returning cached product data");

            // Return cached data
            return Ok(new { Message = "Products fetched from cache", Data = cachedProduct });
        }
    }
}
