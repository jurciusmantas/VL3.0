using RestSharp;

namespace VirtualLibrarity.Database
{
    public static class DatabaseService
    {
        private static readonly string apiUrl = "http://localhost:8080/";

        public static bool Register(User user, string image)
        {
            RestClient client = new RestClient(apiUrl);
            var request = new RestRequest("/register", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new { user, image });

            var res = client.Execute(request);
            if (res.IsSuccessful)
                return true;
            else return false;
        }
    }
}