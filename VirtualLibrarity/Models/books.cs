using System.Collections.Generic;
using Newtonsoft.Json;

namespace VirtualLibrarity.Models
{
    public class books
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Author")]
        public string Author { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Popularity")]
        public int Popularity { get; set; }
        [JsonProperty("Category")]
        public string Category { get; set; }
    }
}