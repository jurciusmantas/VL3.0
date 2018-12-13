using System;
using System.IO;
using System.Threading.Tasks;
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
        public int SendRegisterRequest(string image64String, User user)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("register", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { user, image64String });
            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            try
            {
                Console.Write(response.Content);
                Console.Write(response.StatusCode);
                return int.Parse(response.Content);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Data);
                return -1;
            }
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
        public BookResponse SendBookRequest(string QRCode, bool isTaking)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("api/books", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { QRCode, isTaking});
            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            var book = JsonConvert.DeserializeObject<BookResponse>(response.Content);
            return book;
        }
        public byte[] ConvertToByteArray(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
            return stream.ToArray();
        }
    }
}