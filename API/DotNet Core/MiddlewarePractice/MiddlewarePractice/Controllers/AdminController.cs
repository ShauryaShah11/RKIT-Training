using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewarePractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok(new
            {
                Message = "Welcome to the Admin Dashboard"
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult UserProfile()
        {
            return Ok("Welcome to your profile");
        }
    }
}
