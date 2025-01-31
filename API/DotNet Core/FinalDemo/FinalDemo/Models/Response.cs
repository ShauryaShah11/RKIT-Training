using System.Data;

namespace FinalDemo.Models
{
    public class Response
    {
        public bool IsError { get; set; } = false;
        public string? Message { get; set; }
        public dynamic Data { get; set; }
    }
}
