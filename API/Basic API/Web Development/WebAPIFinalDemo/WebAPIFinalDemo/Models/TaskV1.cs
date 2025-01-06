using System;

namespace WebAPIFinalDemo.Models
{
    /// <summary>
    /// The TaskV1 class represents the model for a task in the system, specifically for API version 1.
    /// </summary>
    public class TaskV1
    {
        /// <summary>
        /// The unique Id Fer Each Task.
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// The title of Task.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Description of Task.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Due Date of Task.
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// The Userd Which has created Task.
        /// </summary>
        public int UserId { get; set; } 
    }
}