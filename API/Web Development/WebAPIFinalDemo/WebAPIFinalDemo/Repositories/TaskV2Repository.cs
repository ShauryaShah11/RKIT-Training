using System;
using System.Collections.Generic;
using System.Linq;
using WebAPIFinalDemo.Enums;
using WebAPIFinalDemo.Models;

namespace WebAPIFinalDemo.Repositories
{
    /// <summary>
    /// Repository class for managing tasks with additional properties such as status and priority.
    /// Provides methods for adding, updating, retrieving, and managing tasks.
    /// </summary>
    public class TaskV2Repository
    {
        #region Private Members
        // Static list to hold tasks in memory for demo purposes.
        private static List<TaskV2> _tasks = new List<TaskV2>
        {
            // Adding 5 dummy tasks for testing and demonstration purposes
            new TaskV2
            {
                TaskId = 1,
                Title = "Task 1",
                Description = "This is the first task with priority and status.",
                DueDate = new DateTime(2025, 2, 20),
                UserId = 1,
                Status = Status.Pending,
                Priority = 1
            },
            new TaskV2
            {
                TaskId = 2,
                Title = "Task 2",
                Description = "This is the second task with priority and status.",
                DueDate = new DateTime(2025, 3, 15),
                UserId = 2,
                Status = Status.Completed,
                Priority = 2
            },
            new TaskV2
            {
                TaskId = 3,
                Title = "Task 3",
                Description = "This is the third task with priority and status.",
                DueDate = new DateTime(2025, 4, 10),
                UserId = 3,
                Status = Status.Pending,
                Priority = 3
            },
            new TaskV2
            {
                TaskId = 4,
                Title = "Task 4",
                Description = "This is the fourth task with priority and status.",
                DueDate = new DateTime(2025, 5, 5),
                UserId = 4,
                Status = Status.Pending,
                Priority = 2
            },
            new TaskV2
            {
                TaskId = 5,
                Title = "Task 5",
                Description = "This is the fifth task with priority and status.",
                DueDate = new DateTime(2025, 6, 25),
                UserId = 5,
                Status = Status.Completed,
                Priority = 1
            }
        };
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all tasks from the repository.
        /// </summary>
        /// <returns>A list of all tasks.</returns>
        public List<TaskV2> GetAllTasks()
        {
            return _tasks;
        }

        /// <summary>
        /// Get all tasks of user by its id.
        /// </summary>
        /// <param name="userId">The id of the user to retrieve tasks.</param>
        /// <returns>The task with userId, or null if not found.</returns>
        public List<TaskV2> GetTasksByUserId(int userId)
        {
            return _tasks.FindAll(t => t.UserId == userId);
        }

        /// <summary>
        /// Gets a task by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>The task with the specified ID, or null if not found.</returns>
        public TaskV2 GetTaskByID(int id)
        {
            return _tasks.FirstOrDefault(t => t.TaskId == id);
        }

        /// <summary>
        /// Adds a new task to the repository.
        /// </summary>
        /// <param name="task">The task to add.</param>
        /// <returns>True if the task was added successfully, false if the task is null or already exists.</returns>
        public bool AddTask(TaskV2 task)
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
        public bool UpdateTask(TaskV2 task)
        {
            TaskV2 existingTask = _tasks.FirstOrDefault(t => t.TaskId == task.TaskId);
            if (existingTask == null)
            {
                return false; // Task does not exist
            }

            // Update the task properties
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.UserId = task.UserId;
            existingTask.Status = task.Status;
            existingTask.Priority = task.Priority;

            return true;
        }

        /// <summary>
        /// Deletes a task by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>True if the task was deleted successfully, false if the task does not exist.</returns>
        public bool DeleteTask(int id)
        {
            TaskV2 taskToRemove = GetTaskByID(id);
            if (taskToRemove == null)
            {
                return false; // Task does not exist
            }

            _tasks.Remove(taskToRemove);
            return true;
        }

        /// <summary>
        /// Update a task status by its id from the repository.
        /// </summary>
        /// <param name="id">The id of the task  to update status</param>
        /// <param name="status">The status with updated information.</param>
        /// <returns>True if Status was updated succesfully, false if task does not exist.</returns>
        public  bool UpdateStatus(int id, Status status)
        {
            TaskV2 task = GetTaskByID(id);
            if(task == null)
            {
                return false;
            }
            task.Status = status;
            return true;
        }
        #endregion
    }
}
