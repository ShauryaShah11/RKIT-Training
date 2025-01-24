using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    public class CustomResultFilter : IResultFilter
    {
        private readonly ILogger<CustomResultFilter> _logger;

        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }

        // This method runs before the action result is executed
        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("Result filter: Executing result...");
        }

        // This method runs after the action result has been executed
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("Result filter: Result executed.");
        }
    }
}
