using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    /// <summary>
    /// Custom result filter that logs messages before and after an action result is executed.
    /// </summary>
    public class CustomResultFilter : IResultFilter
    {
        private readonly ILogger<CustomResultFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResultFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging result filter events.</param>
        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called before the action result is executed.
        /// Logs a message indicating that the result execution is about to start.
        /// </summary>
        /// <param name="context">The <see cref="ResultExecutingContext"/> containing result details.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("Result filter: Executing result...");
        }

        /// <summary>
        /// Called after the action result has been executed.
        /// Logs a message indicating that the result execution has completed.
        /// </summary>
        /// <param name="context">The <see cref="ResultExecutedContext"/> containing result execution details.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("Result filter: Result executed.");
        }
    }
}
