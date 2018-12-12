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
        [JsonProperty ("UserInfo")]
        public User UserInfo { get; set; }
        [JsonProperty("BorrowedBooks")]
        public List<Book> BorrowedBooks { get; set; }
        [JsonProperty("Exception")]
        public string Exception { get; set; }
    }
}