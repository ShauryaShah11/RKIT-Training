using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPISecurity.Attributes;
using WebAPISecurity.Models;
using WebAPISecurity.Repositories;

namespace WebAPISecurity.Controllers
{
    // Enables Cross-Origin Resource Sharing (CORS) for all origins, headers, and methods
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        /// <summary>
        /// Gets a list of all users from the repository.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            // Fetch all users from the repository
            List<User> users = _userRepository.GetAllUsers();

            // Return the list of users in the response with HTTP 200 OK
            return Ok(users);
        }

        /// <summary>
        /// Gets a user by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID or a NotFound response.</returns>
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            // Fetch the user by ID from the repository
            User user = _userRepository.GetUserById(id);

            // If no user is found, return a NotFound (404) response
            if (user == null)
            {
                return NotFound();
            }

            // Return the user details in the response with HTTP 200 OK
            return Ok(user);
        }

        /// <summary>
        /// Gets the current user's information based on the JWT token.
        /// </summary>
        /// <returns>The user corresponding to the claims in the token or an Unauthorized response.</returns>
        [HttpGet]
        [Route("api/user/token")]
        [BearerAuth] // Custom authorization attribute to ensure the user is authenticated
        public IHttpActionResult GetUserbyToken()
        {
            // Access the claims from the currently authenticated user
            var principal = User as ClaimsPrincipal;

            if (principal != null)
            {
                // Extract user details (username, email, userId) from the claims
                string username = principal.FindFirst("sub")?.Value; // "sub" claim (subject)
                string email = principal.FindFirst("email")?.Value; // "email" claim
                int userId = int.Parse(principal.FindFirst("userId")?.Value); // "userId" claim

                // Fetch the user by userId from the repository
                User user = _userRepository.GetAllUsers().FirstOrDefault(u => u.UserId == userId);

                // Return the user details if found
                return Ok(user);
            }

            // If claims are not found, return Unauthorized (401)
            return Unauthorized();
        }

        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="user">The user object containing user details to create.</param>
        /// <returns>A response indicating success or failure of user creation.</returns>
        [HttpPost]
        public IHttpActionResult AddUser([FromBody] User user)
        {
            // Check if the user object is null
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            // Attempt to add the user to the repository
            bool isAdded = _userRepository.AddUser(user);
            if (isAdded)
            {
                // If added successfully, return a Created response with the user details
                return Created($"api/user/{user.UserId}", user);
            }

            // If a user with the same ID already exists, return BadRequest
            return BadRequest("User with the same ID already exists.");
        }

        /// <summary>
        /// Updates an existing user with new data.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>A response indicating success or failure of the update operation.</returns>
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, [FromBody] User user)
        {
            // Ensure the user data is not null and that the provided ID matches the user's ID
            if (user == null || user.UserId != id)
            {
                return BadRequest("User ID mismatch or user data is null.");
            }

            // Attempt to update the user in the repository
            bool isUpdated = _userRepository.UpdateUser(user);
            if (isUpdated)
            {
                // Return success response if the user is updated
                return Ok("User updated successfully.");
            }

            // If the user is not found, return NotFound
            return NotFound();
        }

        /// <summary>
        /// Deletes a user from the system by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A response indicating success or failure of the deletion operation.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            // Attempt to delete the user by ID from the repository
            bool isDeleted = _userRepository.DeleteUser(id);
            if (isDeleted)
            {
                // Return success response if the user is deleted
                return Ok("User deleted successfully.");
            }

            // If the user is not found, return NotFound
            return NotFound();
        }
    }
}
