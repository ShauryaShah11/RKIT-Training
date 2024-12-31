using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System;

namespace WebAPISecurity.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // Log the exception (for debugging purposes)
            // You can log it to a file, database, or logging service

            Exception exception = context.Exception;
            object responseMessage = new
            {
                Message = "An unexpected error occurred.",
                ExceptionMessage = exception.Message,
                exception.StackTrace
            };

            // Return a custom error message as a response
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
        }
    }
}
