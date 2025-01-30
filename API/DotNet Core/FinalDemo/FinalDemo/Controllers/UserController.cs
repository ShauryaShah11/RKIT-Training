using FinalDemo.Enums;
using FinalDemo.Interfaces;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalDemo.Controllers
{
    [Route("api/users")]
    [ApiController] 
    public class UserController : ControllerBase // Use ControllerBase for API-only controllers
    {
        private readonly IUserService _userService;

        // Constructor to initialize the UserService
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
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

        // GET: api/users/{id}
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

        // POST: api/users
        [HttpPost]
        public IActionResult AddUser([FromBody] DTOYMU01 user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            Response addUserResponse = _userService.HandleOperation(user, OperationType.Add);
            if (addUserResponse.IsError)
            {
                return BadRequest(addUserResponse);
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.U01101 }, addUserResponse);
        }

        // PUT: api/users/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateUser(int id, [FromBody] DTOYMU01 user)
        {
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            Response updateUserResponse = _userService.HandleOperation(user, OperationType.Update);
            if (updateUserResponse.IsError)
            {
                return BadRequest(updateUserResponse);
            }
            return Ok(updateUserResponse);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id, [FromBody] DTOYMU01 user)
        {
            Response deleteUserResponse = _userService.HandleOperation(user, OperationType.Delete);
            if (deleteUserResponse.IsError)
            {
                return BadRequest(deleteUserResponse);
            }
            return Ok(deleteUserResponse);
        }
    }
}
