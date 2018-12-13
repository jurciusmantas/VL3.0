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
    public partial class User
    {
        [JsonProperty ("Id")]
        public int Id { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("Firstname")]
        public string Firstname { get; set; }
        [JsonProperty("Lastname")]
        public string Lastname { get; set; }
    }
}