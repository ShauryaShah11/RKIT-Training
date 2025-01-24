using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FilterPractice.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;
        private DateTime _startTime;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        // This method is executed before the action method is executed.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _startTime = DateTime.UtcNow;
        }

        // This method is executed after the action method has executed.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var elapsedTime = DateTime.UtcNow - _startTime;
            _logger.LogInformation($"Action executed in {elapsedTime.TotalMilliseconds} ms");
        }
    }
}
