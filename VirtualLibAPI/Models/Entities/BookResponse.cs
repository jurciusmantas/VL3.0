using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Models.Entities
{
    public class BookResponse
    {
        public books BookInfo { get; set; }
        public bool WasUpdated { get; set; }
    }
}