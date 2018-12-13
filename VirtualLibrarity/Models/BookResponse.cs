using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualLibrarity.Models
{
    public class BookResponse
    {
        [JsonProperty("BookInfo")]
        public Book BookInfo { get; set; }
        [JsonProperty("WasUpdated")]
        public bool WasUpdated { get; set; }
    }
}