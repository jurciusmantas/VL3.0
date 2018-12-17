using System.Collections.Generic;
using VirtualLibrarity.EFModel;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibAPI.Models.Entities
{
    public struct UserToLoginResponse
    {
        public User UserInfo { get; set; }
        public List<Book> BorrowedBooks { get; set; }
        public string ExceptionMessage { get; set; }
    }
}