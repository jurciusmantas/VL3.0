using RestSharp;
using System;

namespace TestApi
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient("http://localhost:8080/");
            var request = new RestRequest("register", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(3);

            var response = client.Execute(request);
            try
            {
                Console.Write(response.Content);
                Console.Write(response.StatusCode);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
