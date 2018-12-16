using Newtonsoft.Json;

namespace VirtualLibrarity.Models
{
    public class Book2
    {
        [JsonProperty("BookInfo")]
        public books BookInfo { get; set; }
        [JsonProperty("Amount")]
        public int Amount { get; set; }
    }
}