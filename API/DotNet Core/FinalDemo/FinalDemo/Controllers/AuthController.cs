using FinalDemo.Helpers;
using FinalDemo.Interfaces;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication, including login functionality.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;

        /// <summary>
        /// Constructor to initialize AuthController with necessary dependencies.
        /// </summary>
        /// <param name="ormLiteDbFactory">Factory to handle database connection.</param>
        /// <param name="jwtHelper">Helper class for generating JWT tokens.</param>
        public AuthController(IUserService userService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Endpoint for user login. Validates user credentials and returns a JWT token.
        /// </summary>
        /// <param name="loginRequest">The login request containing email and password.</param>
        /// <returns>Returns JWT token if login is successful; Unauthorized if credentials are invalid.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            YMU01 user = _userService.Authenticate(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            // Generate JWT token for the authenticated user
            var token = _jwtHelper.GenerateJwtToken(user, "user");

            return Ok(new
            {
                Token = token // Return the generated token
            });
        }
    }

    /// <summary>
    /// Model for login request containing email and password.
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
