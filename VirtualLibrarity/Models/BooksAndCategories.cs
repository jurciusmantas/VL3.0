using System.Collections.Generic;
using Newtonsoft.Json;

namespace VirtualLibrarity.Models
{
    public class BooksAndCategories
    {
        [JsonProperty("Books")]
        List<Book> Books { get; set; }
        [JsonProperty("Categories")]
        string[] Categories { get; set; }
    }
}