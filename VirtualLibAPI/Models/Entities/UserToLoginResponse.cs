using System.Collections.Generic;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Models
{
    public struct UserToLoginResponse
    {
        public User UserInfo { get; set; }
        public List<Book> BorrowedBooks { get; set; }
        public string ExceptionMessage { get; set; }
    }
}