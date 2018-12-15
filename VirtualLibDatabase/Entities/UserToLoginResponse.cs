using System.Collections.Generic;
using VirtualLibrarityDatabase.Entities;

namespace VirtualLibDatabase.Entities
{
    public struct UserToLoginResponse
    {
        public users UserInfo { get; set; }
        public List<Book> BorrowedBooks { get; set; }
        public string ExceptionMessage { get; set; }
    }
}