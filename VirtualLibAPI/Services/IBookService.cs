using System.Collections.Generic;
using VirtualLibAPI.Models.Entities;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibAPI.Services
{
    public interface IBookService
    {
        BookResponse Take(int userId, int qrCode);

        bool Return(int qrCode);

        List<Book> GetUsersBorrowedBooks(int userId);
        List<Book2> GetAllBooks();
    }
}