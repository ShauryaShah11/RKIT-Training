using System.Collections.Generic;
using System.Linq;
using WebAPIFinalDemo.Enums;
using WebAPIFinalDemo.Models;

namespace WebAPIFinalDemo.Repositories
{
    /// <summary>
    /// Repository class for managing tasks.
    /// Provides methods for adding, updating, retrieving, and managing tasks.
    /// </summary>
    public class TaskV1Repository
    {
        #region Private Members
        // Static list to hold tasks in memory for demo purposes.
        private static List<TaskV1> _tasks = new List<TaskV1>
        {
            // Adding 5 dummy tasks for testing and demonstration purposes
            new TaskV1
            {
                TaskId = 1,
                Title = "Task 1",
                Description = "This is the first task.",
                DueDate = new System.DateTime(2025, 2, 20),
                UserId = 1,
            },
            new TaskV1
            {
                TaskId = 2,
                Title = "Task 2",
                Description = "This is the second task.",
                DueDate = new System.DateTime(2025, 3, 15),
                UserId = 2,
            },
            new TaskV1
            {
                TaskId = 3,
                Title = "Task 3",
                Description = "This is the third task.",
                DueDate = new System.DateTime(2025, 4, 10),
                UserId = 3,
            },
            new TaskV1
            {
                TaskId = 4,
                Title = "Task 4",
                Description = "This is the fourth task.",
                DueDate = new System.DateTime(2025, 5, 5),
                UserId = 4,
            },
            new TaskV1
            {
                TaskId = 5,
                Title = "Task 5",
                Description = "This is the fifth task.",
                DueDate = new System.DateTime(2025, 6, 25),
                UserId = 5,
            }
        };
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all tasks from the repository.
        /// </summary>
        /// <returns>A list of all tasks.</returns>
        public List<TaskV1> GetAllTasks()
        {
            return _tasks;
        }

        /// <summary>
        /// Gets a task by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>The task with the specified ID, or null if not found.</returns>
        public TaskV1 GetTaskByID(int id)
        {
            return _tasks.FirstOrDefault(t => t.TaskId == id);
        }

        /// <summary>
        /// Get all tasks of user by its id.
        /// </summary>
        /// <param name="userId">The id of the user to retrieve tasks.</param>
        /// <returns>The task with userId, or null if not found.</returns>
        public List<TaskV1> GetTasksByUserId(int userId)
        {
            return _tasks.FindAll(t => t.UserId == userId);
        }
        /// <summary>
        /// Adds a new task to the repository.
        /// </summary>
        /// <param name="task">The task to add.</param>
        /// <returns>True if the task was added successfully, false if the task is null or already exists.</returns>
        public bool AddTask(TaskV1 task)
        {
            if (task == null || _tasks.Any(t => t.TaskId == task.TaskId))
            {
                return false;
            }
            _tasks.Add(task);
            return true;
        }

        /// <summary>
        /// Updates an existing task in the repository.
        /// </summary>
        /// <param name="task">The task with updated information.</param>
        /// <returns>True if the task was updated successfully, false if the task does not exist.</returns>
        public bool UpdateTask(TaskV1 task)
        {
            TaskV1 existingTask = _tasks.FirstOrDefault(t => t.TaskId == task.TaskId);
            if (existingTask == null)
            {
                return false; // Task does not exist
            }

            // Update the task properties
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.UserId = task.UserId;

            return true;
        }

        /// <summary>
        /// Deletes a task by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>True if the task was deleted successfully, false if the task does not exist.</returns>
        public bool DeleteTask(int id)
        {
            TaskV1 taskToRemove = GetTaskByID(id);
            if (taskToRemove == null)
            {
                return false; // Task does not exist
            }

            _tasks.Remove(taskToRemove);
            return true;
        }
        #endregion
    }
}
