﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIVersioning.Models;

namespace WebAPIVersioning.Controllers
{
    [RoutePrefix("api/v2/students")]
    public class StudentV2Controller : ApiController
    {
        // Sample data for Version 2
        public static readonly List<StudentV2> students = new List<StudentV2>
        {
            new StudentV2 { Id = 1, FirstName = "Alice", LastName = "Johnson" },
            new StudentV2 { Id = 2, FirstName = "Bob", LastName = "Smith" },
            new StudentV2 { Id = 3, FirstName = "Charlie", LastName = "Brown" },
            new StudentV2 { Id = 4, FirstName = "David", LastName = "Lee" },
            new StudentV2 { Id = 5, FirstName = "Eva", LastName = "White" }
        };

        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        public IHttpActionResult GetAllStudent()
        {
            return Ok(students);
        }

        /// <summary>
        /// Gets a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>The student with the specified ID.</returns>
        [HttpGet]
        public IHttpActionResult GetStudentById(int id)
        {
            StudentV2 student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                return Ok(student);
            }
            return NotFound();
        }
    }
}