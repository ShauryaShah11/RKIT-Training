using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebAPISecurity.Filters
{
    /// <summary>
    /// Global Exception Filter to handle unhandled exceptions globally in the Web API.
    /// This filter catches exceptions thrown by actions and returns a custom error response.
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        #region Public Methods
        /// <summary>
        /// This method is triggered when an exception occurs in any API action.
        /// </summary>
        /// <param name="context">The context that contains the exception details.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            // Log the exception details (for debugging and monitoring purposes)
            // You can implement a logging mechanism to log the exception information to a file, database, or external service

            Exception exception = context.Exception;

            // Prepare a custom response message with the error details
            // The message will include the general error message, the exception message, and stack trace
            object responseMessage = new
            {
                Message = "An unexpected error occurred.", // General user-friendly error message
                ExceptionMessage = exception.Message,    // The specific exception message
                exception.StackTrace                      // Stack trace for debugging
            };

            // Set the response to be sent back to the client, with HTTP 500 status code (Internal Server Error)
            // The response includes the error message and details
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
        }
        #endregion
    }
}
