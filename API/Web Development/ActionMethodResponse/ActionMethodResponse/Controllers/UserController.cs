using ActionMethodResponse.Models; // Importing the User model
using ActionMethodResponse.Repositories; // Importing the UserRepository
using System.Net; // Importing HttpStatusCode
using System.Net.Http; // Importing HttpResponseMessage
using System.Web.Http; // Importing ApiController
using System; // Importing Exception
using System.Text;
using System.Collections.Generic; // Importing Encoding

namespace ActionMethodResponse.Controllers
{
    /// <summary>
    /// API Controller for managing users.
    /// </summary>
    public class UserController : ApiController
    {
        // Instance of UserRepository to manage user data
        private readonly UserRepository _userRepository = new UserRepository();

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The user ID to delete.</param>
        [HttpDelete]
        public void DeleteUserById(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user ID to retrieve.</param>
        /// <returns>Returns the user with the specified ID.</returns>
        [HttpGet]
        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        /// <summary>
        /// Retrieves a user with detailed HTTP response customization.
        /// </summary>
        /// <param name="id">The user ID to retrieve.</param>
        /// <returns>Returns an HTTP response message with the user data or an error message if not found.</returns>
        [HttpGet]
        [Route("api/user/message/{id}")]
        public HttpResponseMessage GetUserWithCustomMessage(int id)
        {
            try
            {
                // Initialize response object
                HttpResponseMessage response = new HttpResponseMessage();

                // Retrieve user from repository
                User user = _userRepository.GetUserById(id);

                // Check if user exists
                if (user == null)
                {
                    // Set content and headers for NotFound response
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ReasonPhrase = "User not found";
                    response.Content = new StringContent("The requested user does not exist.", Encoding.UTF8, "text/plain");
                    response.Headers.Add("X-Error-Code", "404");
                    return response;
                }

                // Set content and headers for successful response
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<User>(user, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                response.ReasonPhrase = "User retrieved successfully";
                response.Headers.Add("X-Custom-Header", "OperationSuccess");
                response.Version = new Version(1, 1); // HTTP/1.1

                // Associate the original request message
                response.RequestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/user/message/{id}");

                return response;
            }
            catch (Exception ex)
            {
                // Handle exceptions and set appropriate response properties
                var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"An error occurred: {ex.Message}", Encoding.UTF8, "text/plain"),
                    ReasonPhrase = "Internal Server Error",
                    Version = new Version(1, 1) // HTTP/1.1
                };

                // Add headers to provide additional information
                errorResponse.Headers.Add("X-Error-Code", "500");
                errorResponse.Headers.Add("X-Error-Detail", ex.Message);
                return errorResponse;
            }
        }

        /// <summary>
        /// Retrieves a user with default automatic response formatting.
        /// </summary>
        /// <param name="id">The user ID to retrieve.</param>
        /// <returns>Returns an HTTP response message with the user data or an error message if not found.</returns>
        [HttpGet]
        [Route("api/user/message/{id}")]
        public HttpResponseMessage GetUserWithDefaultMessage(int id)
        {
            User user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user data to add.</param>
        /// <returns>Returns an IHttpActionResult indicating the result of the operation.</returns>
        [HttpPost]
        public IHttpActionResult AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing");
            }
            if (_userRepository.AddUser(user))
            {
                return Created($"api/user/{user.UserId}", user);
            }
            return Conflict();
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">The updated user data.</param>
        /// <returns>Returns an IHttpActionResult indicating the result of the operation.</returns>
        [HttpPut]
        public IHttpActionResult UpdateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing");
            }
            if (_userRepository.UpdateUser(user))
            {
                return Ok(user);
            }
            return NotFound();
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
