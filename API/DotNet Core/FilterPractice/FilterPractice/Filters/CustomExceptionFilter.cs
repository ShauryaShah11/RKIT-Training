using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    /// <summary>
    /// Custom exception filter for handling unhandled exceptions in ASP.NET Core.
    /// Logs exceptions and returns a standardized error response.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging exceptions.</param>
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called when an exception occurs during request processing.
        /// Logs the exception and returns a structured error response.
        /// </summary>
        /// <param name="context">The exception context containing details of the error.</param>
        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unexpected error occurred.");

            // Custom error response
            context.Result = new ObjectResult(new
            {
                Message = "An error occurred while processing your request.",
                Details = context.Exception.Message
            })
            {
                StatusCode = 500 // Internal Server Error
            };

            // Mark exception as handled
            context.ExceptionHandled = true;
        }
    }
}
