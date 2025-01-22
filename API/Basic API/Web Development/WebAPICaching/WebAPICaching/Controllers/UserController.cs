using System.Collections.Generic;
using System.Web.Http;
using WebAPICaching.Models;
using WebAPICaching.Repositories;
using WebAPICaching.Filters;
using Microsoft.Web.Http;
using System.Linq;
using WebAPICaching.Services;

namespace WebAPICaching.Controllers
{
    /// <summary>
    /// The UserController class is an API controller that handles HTTP requests related to user management.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        #region Private Members
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly CacheService _cacheService = new CacheService();
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        [Route("")]
        [ApiVersion("1.0")]
        //[CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetAllUsersV1()
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
        /// Gets first 10 users.
        /// </summary>
        /// <returns>A list of first 10 users.</returns>
        [HttpGet]
        [Route("")]
        [ApiVersion("2.0")]
        [CacheFilter(TimeDuration = 100)]
        public IHttpActionResult GetAllUsersV2()
        {
            List<User> first10users = _userRepository.GetAllUsers().Take(10).ToList();
            return Ok(first10users);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            User user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpPost]
        public IHttpActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null."); // 400 Bad Request
            }

            bool isAdded = _userRepository.AddUser(user);
            if (isAdded)
            {
                return Created($"api/user/{user.UserId}", user); // 201 Created
            }

            return BadRequest("User with the same ID already exists.");
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest("User ID mismatch or user data is null."); // 400 Bad Request
            }

            bool isUpdated = _userRepository.UpdateUser(user);
            if (isUpdated)
            {
                return Ok("User updated successfully."); // 200 OK
            }

            return NotFound(); // 404 Not Found
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            bool isDeleted = _userRepository.DeleteUser(id);
            if (isDeleted)
            {
                return Ok("User deleted successfully."); // 200 OK
            }

            return NotFound(); // 404 Not Found
        }
        #endregion
    }
}
