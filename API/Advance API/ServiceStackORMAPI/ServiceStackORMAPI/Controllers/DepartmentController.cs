using ServiceStackORMAPI.Models;
using ServiceStackORMAPI.Models.DTO;
using ServiceStackORMAPI.Repositories;
using System.Web.Http;

namespace ServiceStackORMAPI.Controllers
{
    /// <summary>
    /// The DepartmentController provides HTTP endpoints for performing operations related to departments.
    /// </summary>
    public class DepartmentController : ApiController
    {
        private readonly DepartmentRepository _departmentRepository;

        /// <summary>
        /// Initializes a new instance of the DepartmentController class.
        /// This constructor initializes the department repository for performing department operations.
        /// </summary>
        public DepartmentController()
        {
            _departmentRepository = new DepartmentRepository();
        }

        /// <summary>
        /// Retrieves all department records from the repository.
        /// Returns an HTTP 200 OK response with the department data, or HTTP 404 NotFound if there is an error.
        /// </summary>
        /// <returns>An HTTP response containing the department data or an error message.</returns>
        [HttpGet]
        public IHttpActionResult GetAllDepartments()
        {
            Response res = _departmentRepository.GetAllDepartments();

            // Check if there is an error and return a NotFound response
            if (res.IsError)
            {
                return NotFound(); // Return NotFound when there's an error
            }

            // Return OK with the retrieved data
            return Ok(res.Data); // Assuming res.Data contains the employee data to be returned
        }

        /// <summary>
        /// Adds a new department to the repository.
        /// Returns an HTTP 201 Created response with the department data if successful, or HTTP 400 BadRequest if there is an error.
        /// </summary>
        /// <param name="dto">The department DTO containing department details to be added.</param>
        /// <returns>An HTTP response indicating the result of the add operation.</returns>
        [HttpPost]
        public IHttpActionResult AddEmployee([FromBody] DTOYMD01 dto)
        {
            Response res = _departmentRepository.HandleOperation(dto, 1);
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = dto.D01101 }, dto);
        }

        /// <summary>
        /// Updates an existing department in the repository.
        /// Returns an HTTP 200 OK response with the updated department data if successful, or HTTP 400 BadRequest if there is an error.
        /// </summary>
        /// <param name="dto">The department DTO containing updated department details.</param>
        /// <returns>An HTTP response indicating the result of the update operation.</returns>
        [HttpPut]
        public IHttpActionResult UpdateEmployee([FromBody] DTOYMD01 dto)
        {
            Response res = _departmentRepository.HandleOperation(dto, 2);
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return Ok(res.Message);
        }

        /// <summary>
        /// Deletes an existing department from the repository.
        /// Returns an HTTP 200 OK response if successful, or HTTP 404 NotFound if there is an error.
        /// </summary>
        /// <param name="dto">The department DTO to be deleted.</param>
        /// <returns>An HTTP response indicating the result of the delete operation.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteEmployee([FromBody] DTOYMD01 dto)
        {
            Response res = _departmentRepository.HandleOperation(dto, 3);

            if (res.IsError)
            {
                return NotFound();
            }
            return Ok(res.Message);
        }
    }
}
