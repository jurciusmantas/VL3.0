using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibAPI.Models.Entities;
using VirtualLibrarity.EFModel;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibAPI.Services
{
    public class BookService : IBookService
    {
        public BookResponse Take(int userId, int qrCode)
        {
            try
            {
                using (var context = new vlEntities())
                {
                    var res = context.copies.Where(c => c.Id == qrCode).FirstOrDefault();
                    res.UserId = userId;
                    context.SaveChanges();

                    var bookInfo = context.books.Where(b => b.Id == res.Id).FirstOrDefault();
                    return new BookResponse
                    {
                        WasUpdated = true,
                        BookInfo = new Book
                        {
                            Author = bookInfo.Author,
                            Title = bookInfo.Title,
                            QRCode = qrCode
                        }
                    };
                }
            }
            catch (Exception)
            {
                return new BookResponse { WasUpdated = false };
            }
        }

        public bool Return(int qrCode)
        {
            try
            {
                using (var context = new vlEntities())
                {
                    var res = context.copies.Where(c => c.Id == qrCode).FirstOrDefault();
                    res.UserId = null;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Book> GetUsersBorrowedBooks(int id)
        {
            using (var context = new vlEntities())
            {
                var res = new List<Book>();
                try
                {
                    var query =
                        from book in context.books
                        join copy in context.copies on book.Id equals copy.BookId
                        where copy.UserId == id
                        select new Book
                        {
                            QRCode = copy.Id,
                            Author = book.Author,
                            Title = book.Title
                        };

                    foreach(var book in query)
                    {
                        res.Add(book);
                    }
                    return res;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<Book2> GetAllBooks()
        {
            var res = new List<Book2>();
            using (var context = new vlEntities())
            {
                try
                {
                    var allBooks = context.books;
                    foreach(var book in allBooks)
                    {
                        res.Add(new Book2
                        {
                            BookInfo = book,
                            Amount = context.copies.Where(c => c.BookId == book.Id)
                                .SkipWhile(c => c.UserId != null).Count()
                        });
                    }
                    return res;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        } 
    }
}
