using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPIFinalDemo.Models;
using WebAPIFinalDemo.Repositories;
using WebAPIFinalDemo.Services;

namespace WebAPIFinalDemo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        /// <summary>
        /// Logs in the user by validating the provided credentials and generating a JWT token.
        /// </summary>
        /// <param name="loginUser">The user object containing the username and password for authentication.</param>
        /// <returns>An HTTP response containing a JWT token if credentials are valid, or an Unauthorized status.</returns>
        // POST: api/auth/login
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] User loginUser)
        {
            // Retrieve the first user that matches the provided username and password
            User user = _userRepository.GetAllUsers()
                .FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

            // If no user is found with the given credentials, return Unauthorized response
            if (user == null)
            {
                return Unauthorized();
            }

            // Generate a token for the authenticated user
            string token = TokenService.GenerateToken(user);

            // Return the generated token in the response body
            return Ok(new { Token = token });
        }
    }
}
