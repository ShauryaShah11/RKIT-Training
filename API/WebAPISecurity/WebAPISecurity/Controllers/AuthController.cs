using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPISecurity.Models;
using WebAPISecurity.Repositories;
using WebAPISecurity.Services;

namespace WebAPISecurity.Controllers
{
    public class AuthController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        // POST: api/auth/login
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] User loginUser)
        {
            User user = _userRepository.GetAllUsers()
                .FirstOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            string token = TokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
