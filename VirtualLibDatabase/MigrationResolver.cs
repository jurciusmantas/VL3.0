using VirtualLibDatabase.Services;

namespace VirtualLibDatabase
{
    public static class MigrationResolver
    {
        public static bool Register(users user)
        {
            var service = new RegisterService();
            return service.Register(user);
        }

        public static users Login(string email, string password)
        {
            var service = new LoginService();
            return service.ManualLogin(email, password);
        }
        
        public static users Login(int id)
        {
            var service = new LoginService();
            return service.FaceRecognitionLogin(id);
        }
    }
}
