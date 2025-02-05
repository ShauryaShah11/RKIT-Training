using FilterPractice.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FilterPractice.Controllers
{
    [ServiceFilter(typeof(CustomExceptionFilter))]
    [Route("api/[contoller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("/profile")]
        [CustomAuthorize("Admin", "Manager")]
        public IActionResult Profile()
        {
            throw new InvalidOperationException("This is a simulated error");
        }
    }
}
