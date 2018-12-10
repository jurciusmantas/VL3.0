using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using RestSharp;

namespace VirtualLibrarity
{
    class RequestSender
    {
        private const string apiUrl = "http://10.3.8.227:45455/api";
        public async Task<int> SendFaceAsync(string image64String, bool isForSave)
        {
            
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("/Registration", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { image64String, isForSave});


            var responseTask = client.ExecuteTaskAsync(request);
            var response = await responseTask;
            try
            {
                Console.Write(response.Content);
                Console.Write(response.StatusCode);
                return int.Parse(response.Content);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Data);
                return 0;
            }
        }
        public void SendLoginFace(string image64String, bool isForSave)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("/faces", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { image64String, isForSave });


            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            try
            {
                Console.Write(response.Content);
                Console.Write(response.StatusCode);
                //return int.Parse(response.Content);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Data);
                //return 0;
            }
        }
        public byte[] ConvertToByteArray(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
            return stream.ToArray();
        }
    }
}