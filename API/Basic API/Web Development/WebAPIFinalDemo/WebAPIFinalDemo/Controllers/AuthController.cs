﻿using System.Linq;
using System.Web.Http;
using WebAPIFinalDemo.Models;
using WebAPIFinalDemo.Repositories;
using WebAPIFinalDemo.Services;

namespace WebAPIFinalDemo.Controllers
{
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    /// <summary>
    /// The AuthController handles authentication-related API endpoints like user login.
    /// </summary>
    public class AuthController : ApiController
    {
        #region Private Members
        /// <summary>
        /// The UserRepository object to handle opration on User model.
        /// </summary>
        private readonly UserRepository _userRepository = new UserRepository();
        #endregion

        #region Public Methods
        /// <summary>
        /// Logs in the user by validating the provided credentials and generating a JWT token.
        /// </summary>
        /// <param name="loginUser">The user object containing the username and password for authentication.</param>
        /// <returns>An HTTP response containing a JWT token if credentials are valid, or an Unauthorized status.</returns>
        // POST: api/auth/login
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] LoginModel loginUser)
        {
            // Retrieve the first user that matches the provided username and password
            User user = _userRepository.GetAllUsers()
                .FirstOrDefault(u => u.Email == loginUser.email && u.Password == loginUser.password);

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
        #endregion
    }
}
