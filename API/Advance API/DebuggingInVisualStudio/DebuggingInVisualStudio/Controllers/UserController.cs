using DebuggingInVisualStudio.Models;
using DebuggingInVisualStudio.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Diagnostics;
using System;

namespace DebuggingInVisualStudio.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        [HttpGet]
        public IHttpActionResult GetAllUser()
        {

            List<User> users = _userRepository.GetAllUser();

            // Example: Conditional Breakpoint
            // Set a conditional breakpoint to pause only if `users.Count > 0`.
            Console.WriteLine(users);
            return Ok(users);
        }

        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            // Debugging Tip: Use a conditional breakpoint to stop only for a specific `id`, e.g., `id == 1`.
            User user = _userRepository.GetUserById(id);

            if (user == null)
            {
                // Breakpoint: Pause here if a user is not found, to simulate and inspect the `NotFound` response.
                //return NotFound();
                #if DEBUG
                    Debug.WriteLine($"user with id {id} not found");
                #endif
                return BadRequest("user not found");
            }

            // Debugging Tip: Use a data inspection point to view the `user` object in detail.
            return Ok(user);

        }

        public IHttpActionResult AddUser([FromBody] User user)
        {
            // Debugging Tip: Set a breakpoint here to inspect the input `user` object before processing.
            bool isAdded = _userRepository.AddUser(user);

            if (isAdded)
            {
                // Breakpoint: Use data inspection to verify that the user was successfully added.
                // Modify the URI dynamically to include the user ID if available.
                return Created($"/api/user/{user.Id}", user);
            }

            // Debugging Tip: Use a standard breakpoint or inspect here when adding a user fails.
            return BadRequest("Failed to add user.");
        }
    }
}
