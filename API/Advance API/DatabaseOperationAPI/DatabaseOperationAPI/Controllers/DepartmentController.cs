using DatabaseOperationAPI.Models;
using DatabaseOperationAPI.Models.POCO;
using DatabaseOperationAPI.Repositories;
using System.Web.Http;

namespace DatabaseOperationAPI.Controllers
{
    /// <summary>
    /// The DepartmentController provides HTTP endpoints for performing CRUD operations on department data.
    /// </summary>
    public class DepartmentController : ApiController
    {
        #region Private Members

        /// <summary>
        /// Repository instance for handling department-related data operations.
        /// </summary>
        private readonly DepartmentRepository _repository = new DepartmentRepository();

        #endregion

        /// <summary>
        /// Retrieves all department records.
        /// </summary>
        /// <returns>
        /// A response containing a list of all departments or an empty list if no data exists.
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetAllDepartment()
        {
            return Ok(_repository.GetDepartmentData());
        }

        /// <summary>
        /// Retrieves a specific department record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the department.</param>
        /// <returns>
        /// A response containing the department details if found, or a 404 error if not found.
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetDepartmentById(int id)
        {
            Response res = _repository.GetDepartmentById(id);
            if (res.IsError)
            {
                return NotFound();
            }
            return Ok(res);
        }

        /// <summary>
        /// Adds a new department record to the database.
        /// </summary>
        /// <param name="dept">The department object containing the details to be added.</param>
        /// <returns>
        /// A response indicating the success or failure of the operation.
        /// </returns>
        [HttpPost]
        public IHttpActionResult AddDepartment([FromBody] YMD01 dept)
        {
            Response res = _repository.AddDepartment(dept);
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = dept.D01F01 }, dept);
        }

        /// <summary>
        /// Updates an existing department record in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the department to be updated.</param>
        /// <param name="dept">The updated department object.</param>
        /// <returns>
        /// A response indicating the success or failure of the update operation.
        /// </returns>
        [HttpPut]
        public IHttpActionResult UpdateDepartment(int id, [FromBody] YMD01 dept)
        {
            Response res = _repository.UpdateDepartment(id, dept);
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return Ok(res);
        }

        /// <summary>
        /// Deletes a specific department record from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the department to be deleted.</param>
        /// <returns>
        /// A response indicating the success or failure of the delete operation.
        /// </returns>
        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Response res = _repository.DeleteDepartment(id);
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return Ok(res.Message);
        }
    }
}
