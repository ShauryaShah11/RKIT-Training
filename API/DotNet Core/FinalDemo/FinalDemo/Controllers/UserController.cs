using FinalDemo.Enums;
using FinalDemo.Helpers;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations such as retrieving, adding, updating, and deleting users.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;

        /// <summary>
        /// Constructor to initialize UserController with the required user service.
        /// </summary>
        /// <param name="userService">User service to handle user-related operations.</param>
        public UserController(IUserService userService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Endpoint to get all users.
        /// </summary>
        /// <returns>Returns a list of all users or an error response if any.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            Response response = _userService.GetAllUsers();
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }

            return Ok(response);
        }

        /// <summary>
        /// Endpoint to get a user by ID.
        /// </summary>
        /// <param name="id">ID of the user to retrieve.</param>
        /// <returns>Returns the user data or a not found response if the user doesn't exist.</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int id)
        {
            Response response = _userService.GetUserById(id);
            if (response.IsError)
            {
                return NotFound(new { Message = response.Message });
            }

            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to add a new user.
        /// </summary>
        /// <param name="dto">User data to be added.</param>
        /// <returns>Returns a created response with the user data or an error response if the creation fails.</returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] DTOYMU01 dto)
        {
            Response response;
            _userService.SetOperationType(EnmOperationType.A);
            YMU01 poco = _userService.PreSave(dto);
            response = _userService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }
            response = _userService.Save(poco);
            return CreatedAtAction(nameof(GetUserById), new { id = poco.U01F01 },
                new { Status = "Success", Message = response.Message});
        }

        /// <summary>
        /// Endpoint to update an existing user.
        /// </summary>
        /// <param name="id">ID of the user to update.</param>
        /// <param name="dto">Updated user data.</param>
        /// <returns>Returns the updated user data or an error response if the update fails.</returns>
        [HttpPut("{id:int}")]
        public IActionResult UpdateUser(int id, [FromBody] DTOYMU01 dto)
        {
            int? userId = _jwtHelper.GetUserIdFromClaims(User);
            if (userId == null || dto.U01F01 != userId.Value)
            {
                return Unauthorized();
            }
            Response response;
            dto.U01F01 = id;
            _userService.SetOperationType(EnmOperationType.U);
            YMU01 poco = _userService.PreSave(dto);
            response = _userService.ValidateOnSave(poco);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }
            response = _userService.Save(poco);
            return Ok(new { Message = response.Message, Data = response.Data });
        }

        /// <summary>
        /// Endpoint to delete a user by ID.
        /// </summary>
        /// <param name="id">ID of the user to delete.</param>
        /// <returns>Returns a success message or an error response if the deletion fails.</returns>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            int? userId = _jwtHelper.GetUserIdFromClaims(User);
            if (userId == null || id != userId.Value)
            {
                return Unauthorized();
            }
            DTOYMU01 dto = new DTOYMU01 { U01F01 = id }; // Assuming U01F01 is the ID field
            _userService.SetOperationType(EnmOperationType.D);
            YMU01 poco = _userService.PreDelete(dto);
            Response response = _userService.ValidateOnDelete(poco);
            if (response.IsError)
            {
                return BadRequest(new { Message = response.Message });
            }
            response = _userService.Delete(poco);
            return Ok(new { Message = response.Message });
        }
    }
}