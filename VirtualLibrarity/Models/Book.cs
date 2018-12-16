using Newtonsoft.Json;
namespace VirtualLibrarity
{
    public partial class Book
    {
        [JsonProperty ("QRCode")]
        public int QRCode { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Author")]
        public string Author { get; set; }
        
    }
}