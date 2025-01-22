using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilterPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok("Welcome to the Admin Dashboard");
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult UserProfile()
        {
            return Ok("Welcome to your profile");
        }
    }
}
