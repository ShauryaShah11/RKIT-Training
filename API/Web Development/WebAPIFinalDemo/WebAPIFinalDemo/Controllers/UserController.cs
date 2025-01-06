using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPIFinalDemo.Models;
using WebAPIFinalDemo.Repositories;
using WebAPIFinalDemo.Attributes;
using WebAPIFinalDemo.Filters;
using Asp.Versioning; // Required for versioning

namespace WebAPIFinalDemo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    [ApiVersion("1.0")] // Indicate that this controller supports version 1.0
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        // Version 1.0 endpoint for getting all users
        [HttpGet]
        [Route("")]
        [ApiVersion("1.0")] // Specify the version for this action
        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetAllUsers()
        {
            List<User> users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        // Version 1.0 endpoint for getting user by ID
        [HttpGet]
        [Route("{id:int}")]
        [ApiVersion("1.0")] // Specify the version for this action
        public IHttpActionResult GetUserById(int id)
        {
            User user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // Token endpoint (no changes, just versioning)
        [HttpGet]
        [Route("token")]
        [ApiVersion("1.0")] // Version for the token action
        [BearerAuth]
        public IHttpActionResult GetUserByToken()
        {
            var principal = User as ClaimsPrincipal;

            if (principal == null)
            {
                return Unauthorized();
            }

            string username = principal.FindFirst("sub")?.Value;
            string email = principal.FindFirst("email")?.Value;
            int userId = int.Parse(principal.FindFirst("userId")?.Value);

            User user = _userRepository.GetUserById(userId);
            return Ok(user);
        }

        // POST method remains the same for version 1.0
        [HttpPost]
        [Route("")]
        [ApiVersion("1.0")]
        public IHttpActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            bool isAdded = _userRepository.AddUser(user);
            if (isAdded)
            {
                return Created($"api/v1.0/user/{user.UserId}", user);
            }

            return BadRequest("User with the same ID already exists.");
        }

        // PUT method remains the same for version 1.0
        [HttpPut]
        [Route("{id:int}")]
        [ApiVersion("1.0")]
        public IHttpActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest("User ID mismatch or user data is null.");
            }

            bool isUpdated = _userRepository.UpdateUser(user);
            if (isUpdated)
            {
                return Ok("User updated successfully.");
            }

            return NotFound();
        }

        // DELETE method remains the same for version 1.0
        [HttpDelete]
        [Route("{id:int}")]
        [ApiVersion("1.0")]
        public IHttpActionResult DeleteUser(int id)
        {
            bool isDeleted = _userRepository.DeleteUser(id);
            if (isDeleted)
            {
                return Ok("User deleted successfully.");
            }

            return NotFound();
        }
    }
}
