using DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    /// <summary>
    /// Controller to handle greeting and cached data requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GreetController : ControllerBase
    {
        private readonly IGreetingService _greetingService;
        private readonly ICacheService _cacheService;
        private readonly ILoggingService _loggingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreetController"/> class.
        /// </summary>
        /// <param name="greetingService">Service for generating greetings.</param>
        /// <param name="cacheService">Service for caching data.</param>
        /// <param name="loggingService">Service for logging messages.</param>
        public GreetController(IGreetingService greetingService, ICacheService cacheService, ILoggingService loggingService)
        {
            _greetingService = greetingService;
            _cacheService = cacheService;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Greets a user with a personalized message.
        /// </summary>
        /// <param name="name">The name of the user to greet.</param>
        /// <returns>A greeting message.</returns>
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

        /// <summary>
        /// Retrieves cached product data or fetches new data if not cached.
        /// </summary>
        /// <returns>Product data, either from cache or freshly fetched.</returns>
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
