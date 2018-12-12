using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace VirtualLibrarity.Models
{
    public class BookFromResponse
    {
        [JsonProperty("Book")]
        public Book Book { get; set; }
        [JsonProperty("ReturnDate")]
        public DateTime? ReturnDate { get; set; }
    }
}