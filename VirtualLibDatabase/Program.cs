using VirtualLibDatabase.Services;

namespace VirtualLibDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterService service = new RegisterService();
            service.Register(new users
            {
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Password = "test"
            });
        }
    }
}
