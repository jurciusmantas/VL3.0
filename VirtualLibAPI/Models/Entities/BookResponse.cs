using VirtualLibrarity.Models.Entities;
using VirtualLibrarityDatabase.Entities;

namespace VirtualLibAPI.Models.Entities
{
    public class BookResponse
    {
        public Book BookInfo { get; set; }
        public bool WasUpdated { get; set; }
    }
}