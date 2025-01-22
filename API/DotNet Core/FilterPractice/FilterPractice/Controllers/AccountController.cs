using FilterPractice.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilterPractice.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        [HttpPost("/account/login")]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            if(loginModel.Username == "admin@example.com" && loginModel.Password == "Admin123")
            {
                // Generate JWT Token
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, loginModel.Username),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890abcdef1234567890abcdef"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "your_issuer",
                    audience: "your_audience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized();
        }

        [HttpPost("/account/logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies");
            return Ok("Logged out");
        }
    }
}
