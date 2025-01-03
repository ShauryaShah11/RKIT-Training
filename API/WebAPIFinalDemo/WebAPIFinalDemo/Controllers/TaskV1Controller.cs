using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPIFinalDemo.Attributes;
using WebAPIFinalDemo.Models;
using WebAPIFinalDemo.Repositories;

namespace WebAPIFinalDemo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1/tasks")]
    public class TaskV1Controller : ApiController
    {
        private readonly TaskV1Repository _taskV1Repository = new TaskV1Repository();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllTasks()
        {
            List<TaskV1> tasks = _taskV1Repository.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetTaskById(int id)
        {
            TaskV1 task = _taskV1Repository.GetTaskByID(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [Route("")]
        [BearerAuth]
        public IHttpActionResult AddTask(TaskV1 task)
        {
            if (task == null)
            {
                return BadRequest("Task cannot be null");
            }

            var principal = User as ClaimsPrincipal;
            int id = int.Parse(principal.FindFirst("userId")?.Value);
            task.UserId = id;
            bool isAdded = _taskV1Repository.AddTask(task);
            if (isAdded)
            {
                return Created($"api/tasks/v1/{task.TaskId}", task);
            }
            return BadRequest("Task with the same Id already exists");
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateTask(TaskV1 task)
        {
            if (task == null)
            {
                return BadRequest("Task cannot be null");
            }
            bool isUpdated = _taskV1Repository.UpdateTask(task);
            if (isUpdated)
            {
                return Ok("Task updated successfully");
            }
            return BadRequest("Failed to update task");
        }

        [HttpDelete]
        [Route("{id:int}")]
        [BearerAuth]
        public IHttpActionResult DeleteTask(int id)
        {
            var principal = User as ClaimsPrincipal;
            int userId = int.Parse(principal.FindFirst("userId")?.Value);

            TaskV1 task = _taskV1Repository.GetTaskByID(id);
            if (task.UserId != userId)
            {
                return Unauthorized();
            }
            bool isDeleted = _taskV1Repository.DeleteTask(id);
            if (isDeleted)
            {
                return Ok("Task deleted successfully");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("mytasks")]
        [BearerAuth]
        public IHttpActionResult GetMyTasks()
        {
            // Get the current user's ID from the claims
            var principal = User as ClaimsPrincipal;
            var userIdClaim = principal?.FindFirst("userId");

            if (userIdClaim == null)
            {
                return Unauthorized();  // UserId claim is missing
            }

            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return Unauthorized();  // Invalid userId format
            }

            // Fetch tasks for the current user
            List<TaskV1> tasks = _taskV1Repository.GetTasksByUserId(userId);

            if (tasks == null || tasks.Count == 0)
            {
                return NotFound();  // No tasks found for this user
            }

            return Ok(tasks);  // Return the tasks
        }

    }
}
