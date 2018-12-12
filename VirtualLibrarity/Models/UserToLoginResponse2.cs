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
using VirtualLibrarity.Models;

namespace VirtualLibrarity
{ 
    public partial class UserToLoginResponse2
    {
        [JsonProperty ("userInfo")]
        public User UserInfo { get; set; }
        [JsonProperty("borrowedBook")]
        public BookFromResponse[] BorrowedBooks { get; set; }
        [JsonProperty("exception")]
        public string Exception { get; set; }
    }
}