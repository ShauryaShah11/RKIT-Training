using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIVersioning.Models;

namespace WebAPIVersioning.Controllers
{
    /// <summary>
    /// The StudentV1Controller is an API controller that handles HTTP endpoints for managing students, specifically for API version v1.
    /// </summary>
    [RoutePrefix("api/v1/students")]
    public class StudentV1Controller : ApiController
    {
        #region Private Members
        private static readonly List<StudentV1> students = new List<StudentV1>
        {
            new StudentV1 { Id = 1, Name = "Alice Johnson" },
            new StudentV1 { Id = 2, Name = "Bob Smith" },
            new StudentV1 { Id = 3, Name = "Charlie Brown" },
            new StudentV1 { Id = 4, Name = "David Lee" },
            new StudentV1 { Id = 5, Name = "Eva White" }
        };
        #endregion

        #region Public Methods

        [HttpGet]
        public IHttpActionResult GetAllStudent()
        {
            return Ok(students);
        }

        [HttpGet]
        public IHttpActionResult GetStudentById(int id)
        {
            StudentV1 student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                return Ok(student);
            }
            return NotFound();
        }
        #endregion
    }
}
