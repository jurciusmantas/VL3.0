using System.Collections.Generic;

namespace VirtualLibDatabase.Entities
{
    public struct UserToLoginResponse
    {
        public users UserInfo { get; set; }
        public List<books> BorrowedBooks { get; set; }
        public string ExceptionMessage { get; set; }
    }
}