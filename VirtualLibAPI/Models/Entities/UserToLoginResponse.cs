using System.Collections.Generic;
using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Models.Entities
{
    public struct UserToLoginResponse
    {
        public users UserInfo { get; set; }
        public List<books> BorrowedBooks { get; set; }
        public string ExceptionMessage { get; set; }
    }
}