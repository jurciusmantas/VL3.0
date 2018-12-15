using System.Collections.Generic;

namespace VirtualLibDatabase.Entities
{
    public struct UserToLoginResponse
    {
        public User UserInfo { get; set; }
        public List<Book> BorrowedBooks { get; set; }
        public string ExceptionMessage { get; set; }
    }
}