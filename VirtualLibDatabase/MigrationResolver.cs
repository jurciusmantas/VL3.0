using System.Collections.Generic;
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

        public static bool TakeBook(int userId, int qrCode)
        {
            var service = new BookService();
            return service.Take(userId, qrCode);
        }

        public static bool ReturnBook(int qrCode)
        {
            var service = new BookService();
            return service.Return(qrCode);
        }

        public static List<books> GetAllBookList()
        {
            var service = new BookService();
            return service.GetAllBooks();
        }
    }
}
