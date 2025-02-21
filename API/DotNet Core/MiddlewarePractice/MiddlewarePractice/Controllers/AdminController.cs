using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewarePractice.Controllers
{
    /// <summary>
    /// Controller for admin-related actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// Gets the admin dashboard.
        /// </summary>
        /// <returns>A welcome message for the admin dashboard.</returns>
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok(new
            {
                Message = "Welcome to the Admin Dashboard"
            });
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <returns>A welcome message for the user profile.</returns>
        [Authorize]
        [HttpGet("profile")]
        public IActionResult UserProfile()
        {
            return Ok("Welcome to your profile");
        }
    }
}