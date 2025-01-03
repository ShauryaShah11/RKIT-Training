using System;

namespace WebAPIFinalDemo.Models
{
    public class TaskV1
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId { get; set; } 
    }
}