using System.Collections.Generic;
using VirtualLibDatabase;

namespace VirtualLibAPI.Model.Entities
{
    public struct UserToLoginResponse
    {
        public users UserInfo { get; set; }
        public List<books> BorrowedBooks { get; set; }
        public string ExceptionMessage { get; set; }
    }
}