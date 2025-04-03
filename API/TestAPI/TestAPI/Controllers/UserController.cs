using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TestAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private static readonly List<User> _users = new List<User>();

        static UserController()
        {
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "San Francisco", "Miami", "Dallas", "Seattle", "Denver" };
            Random random = new Random();

            // Populate the list with 30 dummy records
            for (int i = 1; i <= 30; i++)
            {
                _users.Add(new User
                {
                    id = i,
                    firstname = "User" + i,
                    username = "user" + i + "@example.com",
                    city = cities[random.Next(cities.Length)], // Assign city randomly
                    salary = random.Next(30000, 100000) // Random salary between 30,000 and 100,000
                });
            }
        }

        // GET: User
        [HttpGet]
        public IHttpActionResult GetUser()
        {
            return Ok(_users);
        }
    }

    public class User
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string username { get; set; }
        public string city { get; set; }
        public int salary { get; set; }
    }
}
