using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIVersioning.Models;

namespace WebAPIVersioning.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        public static readonly List<StudentV1> students1 = new List<StudentV1>
        {
            new StudentV1 { Id = 1, Name = "Alice Johnson" },
            new StudentV1 { Id = 2, Name = "Bob Smith" },
            new StudentV1 { Id = 3, Name = "Charlie Brown" },
            new StudentV1 { Id = 4, Name = "David Lee" },
            new StudentV1 { Id = 5, Name = "Eva White" }
        };

        // Sample data for Version 2
        public static readonly List<StudentV2> students2 = new List<StudentV2>
        {
            new StudentV2 { Id = 1, FirstName = "Alice", LastName = "Johnson" },
            new StudentV2 { Id = 2, FirstName = "Bob", LastName = "Smith" },
            new StudentV2 { Id = 3, FirstName = "Charlie", LastName = "Brown" },
            new StudentV2 { Id = 4, FirstName = "David", LastName = "Lee" },
            new StudentV2 { Id = 5, FirstName = "Eva", LastName = "White" }
        };

        [HttpGet]
        [ApiVersion("1.0")]
        public IHttpActionResult GetAllStudentsV1()
        {
            return Ok(students1);
        }

        [HttpGet]
        [ApiVersion("2.0")]
        public IHttpActionResult GetAllStudentsV2()
        {
            return Ok(students2);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public IHttpActionResult GetStudentByIdV1(int id)
        {
            StudentV1 student= students1.FirstOrDefault(s => s.Id == id);
            return Ok(student);
        }

        [HttpGet]
        [ApiVersion("2.0")]
        public IHttpActionResult GetStudentByIdV2(int id)
        {
            StudentV2 studnet = students2.FirstOrDefault(s => s.Id == id);
            return Ok(studnet);

        }
    }
}
