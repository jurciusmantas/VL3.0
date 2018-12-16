using System.IO;
using Android.Graphics;
using Newtonsoft.Json;
using RestSharp;
using VirtualLibrarity.Models;

namespace VirtualLibrarity
{
    class RequestSender
    {
        private const string apiUrl = "http://192.168.0.182:45455/";

        public UserToLoginResponse2 SendLoginRequest(string image64String)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("api/faces", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { image64String });
            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            var result = JsonConvert.DeserializeObject<UserToLoginResponse2>(response.Content);
            return result;
        }
        public int SendRegisterRequest(string image, User user)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("register", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { user, image });
            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            return int.Parse(response.Content);
        }
        public UserToLoginResponse2 SendLoginRequest(string email, string password)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("login/byargs", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { email, password });
            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            var result = JsonConvert.DeserializeObject<UserToLoginResponse2>(response.Content);
            return result;
        }
        public BookResponse SendUpdateCopiesRequest(string QRCode, bool isTaking, int userId)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("api/books", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { QRCode, isTaking, userId});
            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            var book = JsonConvert.DeserializeObject<BookResponse>(response.Content);
            return book;
        }
        public BooksAndCategories SendGetBooksAndCategoriesRequest()
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("api/books", Method.GET);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<BooksAndCategories>(response.Content);
        }
        public byte[] ConvertToByteArray(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
            return stream.ToArray();
        }
    }
}