using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalDemo.Filters
{
    /// <summary>
    /// Custom exception filter to handle unhandled exceptions globally in the application.
    /// It logs the exception details and returns a standardized error message in the response.
    /// The response includes a general error message and the specific exception message for debugging purposes.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Handles exceptions thrown during the request processing pipeline.
        /// Logs the exception and returns a custom error response.
        /// </summary>
        /// <param name="context">The context containing information about the exception.</param>
        public void OnException(ExceptionContext context)
        {
            // Log the exception or handle specific exceptions
            var exception = context.Exception;

            // You can log the exception to a logging service, for example:
            Console.WriteLine($"Exception occurred: {exception.Message}");

            // Set the result to a custom response
            var result = new ObjectResult(new
            {
                message = "An unexpected error occurred. Please try again later.",
                exceptionMessage = exception.Message
            })
            {
                StatusCode = 500 // Internal Server Error
            };

            context.Result = result; // Return custom response
        }
    }


}
