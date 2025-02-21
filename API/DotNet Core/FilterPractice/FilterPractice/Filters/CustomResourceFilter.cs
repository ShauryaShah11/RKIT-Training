using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    /// <summary>
    /// Custom resource filter that logs messages before and after the execution of an action.
    /// </summary>
    public class CustomResourceFilter : IResourceFilter
    {
        private readonly ILogger<CustomResourceFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResourceFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging resource filter events.</param>
        public CustomResourceFilter(ILogger<CustomResourceFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called before the action method executes.
        /// Logs a message indicating the start of resource execution.
        /// </summary>
        /// <param name="context">The <see cref="ResourceExecutingContext"/> containing request details.</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation("Resource filter: Executing action...");
        }

        /// <summary>
        /// Called after the action method has executed.
        /// Logs a message indicating the completion of resource execution.
        /// </summary>
        /// <param name="context">The <see cref="ResourceExecutedContext"/> containing response details.</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation("Resource filter: Action executed.");
        }
    }
}
