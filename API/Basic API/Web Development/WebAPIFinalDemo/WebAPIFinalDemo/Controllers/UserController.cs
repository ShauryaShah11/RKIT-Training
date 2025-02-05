using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPIFinalDemo.Models;
using WebAPIFinalDemo.Repositories;
using WebAPIFinalDemo.Attributes;
using WebAPIFinalDemo.Filters;
using Asp.Versioning; // Required for versioning
using WebAPIFinalDemo.Services;

namespace WebAPIFinalDemo.Controllers
{
    /// <summary>
    /// The UserController provides HTTP endpoints for performing operations related to users, specifically for version 1.0.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    [ApiVersion("1.0")] // Indicate that this controller supports version 1.0
    public class UserController : ApiController
    {
        #region Private Members
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly CacheService _cacheService = new CacheService();
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves all users from the repository.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        [HttpGet]
        [Route("all")]
        [ApiVersion("1.0")] // Specify the version for this action
        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetAllUsers()
        {
            string cacheKey = "users";

            // Retrieve the cached data as a List<User>
            List<User> users = _cacheService.Get<List<User>>(cacheKey);
            if (users != null)
            {
                return Ok(users); // Return cached users if available
            }

            // Fetch users from the repository if not found in cache
            users = _userRepository.GetAllUsers();

            // Cache the users as a List<User>
            _cacheService.Set<List<User>>(cacheKey, users, 600);

            return Ok(users); // Return the fetched users
        }

        /// <summary>
        /// Retrieves a specific user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Returns the user if found, or a NotFound response if the user does not exist.</returns>
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

        /// <summary>
        /// Retrieves a user based on the token information of the authenticated user.
        /// </summary>
        /// <returns>Returns the user associated with the authenticated token, or Unauthorized if the token is invalid or missing.</returns>
        [HttpGet]
        [Route("token")]
        [ApiVersion("1.0")] // Version for the token action
        [BearerAuth]
        public IHttpActionResult GetUserByToken()
        {
            ClaimsPrincipal principal = User as ClaimsPrincipal;

            if (principal == null)
            {
                return Unauthorized();
            }

            string username = UserClaimService.GetUsername(principal);
            string email = UserClaimService.GetEmail(principal);
            int userId = UserClaimService.GetUserId(principal);

            User user = _userRepository.GetUserById(userId);
            return Ok(user);
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user object to be added.</param>
        /// <returns>Returns a Created response if the user is added successfully, or a BadRequest response if the user already exists.</returns>
        [HttpPost]
        [Route("add")]
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

        /// <summary>
        /// Updates an existing user by their unique ID with new data.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>Returns an Ok response if the user is updated successfully, or a BadRequest/NotFound response based on the outcome.</returns>
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

        /// <summary>
        /// Deletes a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns a success message if the user is deleted, or a NotFound response if the user does not exist.</returns>
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
        #endregion
    }
}
