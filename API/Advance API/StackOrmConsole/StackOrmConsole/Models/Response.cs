using System.Data;

namespace StackOrmConsole.Models
{
    public class Response
    {
        public DataTable Data { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; } = false;
    }
}
