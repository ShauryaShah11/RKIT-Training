using FilterPractice.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FilterPractice.Controllers
{
    /// <summary>
    /// Controller for user-related operations.
    /// </summary>
    [ServiceFilter(typeof(CustomExceptionFilter))] // Ensures exception handling is applied
    [Route("api/[controller]")] 
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Gets the profile information. Only accessible to Admin and Manager roles.
        /// </summary>
        /// <returns>Profile details or an error response.</returns>
        [HttpGet("profile")]
        [CustomAuthorize("Admin", "Manager")]
        public IActionResult Profile()
        {
            throw new InvalidOperationException("This is a simulated error");
        }
    }
}
