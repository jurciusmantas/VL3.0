using VirtualLibDatabase.Services;

namespace VirtualLibDatabase
{
    public static class MigrationResolver
    {
        public static bool Register(users user)
        {
            var service = new RegisterService();
            try
            {
                service.Register(user);
            }
        }

        public static 
    }
}
