using System;
using WebAPIFinalDemo.Enums;

namespace WebAPIFinalDemo.Models
{
    /// <summary>
    /// Represents a task with additional properties like status and priority.
    /// This class is used to manage tasks with details like TaskId, Title, Description, DueDate, 
    /// UserId, Status, and Priority.
    /// </summary>
    public class TaskV2
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the title of the task.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the due date of the task.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who created or is assigned the task.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the status of the task (e.g., Pending, Completed).
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the priority of the task (e.g., low, medium, high).
        /// </summary>
        public int Priority { get; set; }
    }
}
