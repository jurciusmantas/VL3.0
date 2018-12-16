using Newtonsoft.Json;

namespace VirtualLibAPI.Models.Entities
{
    public partial class Book
    {
        public int QRCode { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        
    }
}