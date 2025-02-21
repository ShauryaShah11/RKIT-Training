using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations such as retrieving, adding, updating, and deleting users.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase // Use ControllerBase for API-only controllers
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor to initialize UserController with the required user service.
        /// </summary>
        /// <param name="userService">User service to handle user-related operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Endpoint to get all users.
        /// </summary>
        /// <returns>Returns a list of all users or an error response if any.</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            Response userResponse = _userService.GetAllUsers();
            if (userResponse.IsError)
            {
                return BadRequest(userResponse);
            }
            return Ok(userResponse);
        }

        /// <summary>
        /// Endpoint to get a user by ID.
        /// </summary>
        /// <param name="id">ID of the user to retrieve.</param>
        /// <returns>Returns the user data or a not found response if the user doesn't exist.</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetUser(int id)
        {
            Response userResponse = _userService.GetUserById(id);
            if (userResponse.IsError)
            {
                return NotFound(userResponse); // Use NotFound for missing users
            }
            return Ok(userResponse);
        }

        /// <summary>
        /// Endpoint to add a new user.
        /// </summary>
        /// <param name="user">User data to be added.</param>
        /// <returns>Returns a created response with the user data or an error response if the creation fails.</returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] DTOYMU01 user)
        {
            
            Response addUserResponse = _userService.PreSave(user);
            if (addUserResponse.IsError)
            {
                return BadRequest(addUserResponse);
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.U01F01 }, addUserResponse);
        }

        /// <summary>
        /// Endpoint to update an existing user.
        /// </summary>
        /// <param name="id">ID of the user to update.</param>
        /// <param name="user">Updated user data.</param>
        /// <returns>Returns the updated user data or an error response if the update fails.</returns>
        [HttpPut("{id:int}")]
        public IActionResult UpdateUser(int id, [FromBody] DTOYMU01 user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            Response updateUserResponse = _userService.HandleOperation(user, EnmOperationType.Update);
            if (updateUserResponse.IsError)
            {
                return BadRequest(updateUserResponse);
            }
            return Ok(updateUserResponse);
        }

        /// <summary>
        /// Endpoint to delete a user by ID.
        /// </summary>
        /// <param name="id">ID of the user to delete.</param>
        /// <param name="user">User data to be deleted.</param>
        /// <returns>Returns a response indicating success or failure of the deletion.</returns>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id, [FromBody] DTOYMU01 user)
        {
            Response deleteUserResponse = _userService.HandleOperation(user, EnmOperationType.Delete);
            if (deleteUserResponse.IsError)
            {
                return BadRequest(deleteUserResponse);
            }
            return Ok(deleteUserResponse);
        }
    }
}

