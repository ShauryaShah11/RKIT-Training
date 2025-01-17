using ServiceStackORMAPI.Models;
using ServiceStackORMAPI.Models.DTO;
using ServiceStackORMAPI.Repositories;
using System.Web.Http;

namespace ServiceStackORMAPI.Controllers
{
    /// <summary>
    /// The EmployeeController provides HTTP endpoints for performing operations related to employees.
    /// </summary>
    public class EmployeeController : ApiController
    {
        private readonly EmployeeRepository _employeeRepository;

        /// <summary>
        /// Initializes a new instance of the EmployeeController class.
        /// This constructor initializes the employee repository for performing employee operations.
        /// </summary>
        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository();
        }

        /// <summary>
        /// Retrieves all employee records from the repository.
        /// Returns an HTTP 200 OK response with the employee data, or HTTP 404 NotFound if there is an error.
        /// </summary>
        /// <returns>An HTTP response containing the employee data or an error message.</returns>
        [HttpGet]
        public IHttpActionResult GetAllEmployee()
        {
            Response res = _employeeRepository.GetAllEmployees();

            // Check if there is an error and return a NotFound response
            if (res.IsError)
            {
                return NotFound(); // Return NotFound when there's an error
            }

            // Return OK with the retrieved data
            return Ok(res.Data); // Assuming res.Data contains the employee data to be returned
        }

        /// <summary>
        /// Adds a new employee to the repository.
        /// Returns an HTTP 201 Created response with the employee data if successful, or HTTP 400 BadRequest if there is an error.
        /// </summary>
        /// <param name="dto">The employee DTO containing employee details to be added.</param>
        /// <returns>An HTTP response indicating the result of the add operation.</returns>
        [HttpPost]
        public IHttpActionResult AddEmployee([FromBody] DTOYME01 dto)
        {
            Response res = _employeeRepository.HandleOperation(dto, 1);
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = dto.E01101 }, dto);
        }

        /// <summary>
        /// Updates an existing employee in the repository.
        /// Returns an HTTP 200 OK response with the updated employee data if successful, or HTTP 400 BadRequest if there is an error.
        /// </summary>
        /// <param name="dto">The employee DTO containing updated employee details.</param>
        /// <returns>An HTTP response indicating the result of the update operation.</returns>
        [HttpPut]
        public IHttpActionResult UpdateEmployee([FromBody] DTOYME01 dto)
        {
            Response res = _employeeRepository.HandleOperation(dto, 2);
            if (res.IsError)
            {
                return BadRequest(res.Message);
            }
            return Ok(res.Message);
        }

        /// <summary>
        /// Deletes an existing employee from the repository.
        /// Returns an HTTP 200 OK response if successful, or HTTP 404 NotFound if there is an error.
        /// </summary>
        /// <param name="dto">The employee DTO to be deleted.</param>
        /// <returns>An HTTP response indicating the result of the delete operation.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteEmployee([FromBody] DTOYME01 dto)
        {
            Response res = _employeeRepository.HandleOperation(dto, 3);

            if (res.IsError)
            {
                return NotFound();
            }
            return Ok(res.Message);
        }
    }
}
