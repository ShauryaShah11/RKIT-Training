using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    /// <summary>
    /// Custom action filter that logs the execution time of an action.
    /// </summary>
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;
        private DateTime _startTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called before the action method is executed.
        /// </summary>
        /// <param name="context">The context for the action being executed.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _startTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Called after the action method has executed.
        /// Logs the time taken to execute the action.
        /// </summary>
        /// <param name="context">The context for the executed action.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var elapsedTime = DateTime.UtcNow - _startTime;
            _logger.LogInformation($"Action executed in {elapsedTime.TotalMilliseconds} ms");
        }
    }
}
