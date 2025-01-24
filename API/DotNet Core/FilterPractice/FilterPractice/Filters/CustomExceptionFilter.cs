using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterPractice.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        // Constructor to inject logger
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unexpected error occurred.");

            // You can customize your error response here
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
