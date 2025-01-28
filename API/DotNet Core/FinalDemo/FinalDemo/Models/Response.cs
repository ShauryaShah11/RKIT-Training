using System.Data;

namespace FinalDemo.Models
{
    public class Response
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public DataTable Data { get; set; }
    }
}
