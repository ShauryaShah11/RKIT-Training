using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    public class CustomResourceFilter : IResourceFilter
    {
        private readonly ILogger<CustomResourceFilter> _logger;

        public CustomResourceFilter(ILogger<CustomResourceFilter> logger)
        {
            _logger = logger;
        }

        // This method is executed before the action is executed.
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation("Resource filter: Executing action...");
        }

        // This method is executed after the action has executed.
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation("Resource filter: Action executed.");
        }
    }
}
