using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.Threading.Tasks;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Interfaces;

namespace VirtualLibAPI
{
    public class FacePlusRequest:IRequest
    {
         public T MakeRequestAsync<T>(string image1,string image2)
            where T:IResponse
        {
            var client = new RestClient(Strings.GetString("clientUrl"));
            var request = new RestRequest("", Method.POST,DataFormat.Json);
            request.AddParameter("api_key", Strings.GetString("apiKey"));
            request.AddParameter("api_secret", Strings.GetString("apiSecret"));
            request.AddParameter("image_base64_1", image1);
            request.AddParameter("image_base64_2", image2);
            var responseTask = client.ExecuteTaskAsync(request);
            var response = responseTask.Result;
            return  JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}