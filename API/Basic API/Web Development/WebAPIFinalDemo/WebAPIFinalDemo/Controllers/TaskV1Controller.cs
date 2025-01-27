using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http.Cors;
using System.Web.Http;
using WebAPIFinalDemo.Attributes;
using WebAPIFinalDemo.Models;
using WebAPIFinalDemo.Repositories;
using WebAPIFinalDemo.Services;

namespace WebAPIFinalDemo.Controllers
{
    /// <summary>
    /// The TaskV1Controller provides HTTP endpoints for performing operations on tasks, specifically for API version 1.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1/tasks")]
    public class TaskV1Controller : ApiController
    {
        #region Private Members
        private readonly TaskV1Repository _taskV1Repository = new TaskV1Repository();
        private readonly CacheService _cacheService = new CacheService();
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves all tasks from the repository.
        /// </summary>
        /// <returns>Returns a list of all tasks.</returns>
        [HttpGet]
        [Route("all")]
        [BearerAuth]
        public IHttpActionResult GetAllTasks()
        {
            string cacheKey = "tasksv1";

            // Retrieve the cached data as a List<User>
            List<TaskV1> tasks = _cacheService.Get<List<TaskV1>>(cacheKey);
            if (tasks != null)
            {
                return Ok(tasks); // Return cached users if available
            }

            // Fetch users from the repository if not found in cache
            tasks = _taskV1Repository.GetAllTasks();

            // Cache the users as a List<User>
            _cacheService.Set<List<TaskV1>>(cacheKey, tasks, 600);

            return Ok(tasks); // Return the fetched tasks
        }

        /// <summary>
        /// Retrieves a task by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>Returns the task if found, or a NotFound response if the task does not exist.</returns>
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

        /// <summary>
        /// Adds a new task to the system.
        /// </summary>
        /// <param name="task">The task object to be added.</param>
        /// <returns>Returns a Created response if the task is added successfully, or a BadRequest response if the task already exists.</returns>
        [HttpPost]
        [Route("add")]
        [BearerAuth]
        public IHttpActionResult AddTask([FromBody]TaskV1 task)
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

        /// <summary>
        /// Updates an existing task with new information.
        /// </summary>
        /// <param name="task">The updated task data.</param>
        /// <returns>Returns an Ok response if the task is updated successfully, or a BadRequest response if the update fails.</returns>
        [HttpPut]
        [Route("update")]
        public IHttpActionResult UpdateTask([FromBody]TaskV1 task)
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

        /// <summary>
        /// Deletes a task by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>Returns a success message if the task is deleted, or a NotFound/Unauthorized response if the task does not exist or the user is not authorized to delete it.</returns>
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

        /// <summary>
        /// Retrieves all tasks that belong to the currently authenticated user.
        /// </summary>
        /// <returns>Returns a list of tasks for the authenticated user, or a NotFound response if no tasks exist.</returns>
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
        #endregion
    }
}
