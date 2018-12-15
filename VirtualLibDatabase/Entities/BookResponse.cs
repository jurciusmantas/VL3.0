using VirtualLibrarityDatabase.Entities;

namespace VirtualLibDatabase
{
    public class BookResponse
    {
        public Book BookInfo { get; set; }
        public bool WasUpdated { get; set; }
    }
}