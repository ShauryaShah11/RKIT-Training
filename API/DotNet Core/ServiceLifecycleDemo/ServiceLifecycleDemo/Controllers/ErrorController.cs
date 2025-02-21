using Microsoft.AspNetCore.Mvc;

namespace ServiceLifecycleDemo.Controllers
{
    /// <summary>
    /// Controller responsible for handling application errors.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Handles unexpected errors and returns a standardized error response.
        /// </summary>
        /// <returns>A standardized problem response indicating an internal server error.</returns>
        [HttpGet]
        public IActionResult HandleError()
        {
            return Problem(
                detail: "An unexpected error occurred. Please try again later.",
                title: "Internal Server Error"
            );
        }
    }
}
