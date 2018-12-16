using System.Data;

namespace VirtualLibDatabase.Entities
{
    public class DbResponse
    {
        public DataTable Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}