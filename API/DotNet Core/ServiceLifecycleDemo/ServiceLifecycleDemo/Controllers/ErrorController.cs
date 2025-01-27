using Microsoft.AspNetCore.Mvc;

namespace ServiceLifecycleDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
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
