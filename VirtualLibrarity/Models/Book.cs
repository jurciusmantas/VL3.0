
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

namespace VirtualLibrarity
{
    public partial class Book
    {
        [JsonProperty ("QRCode")]
        public string QRCode { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}