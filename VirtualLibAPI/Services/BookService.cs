using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Services
{
    public class BookService : IBookService
    {
        public bool Take(int userId, int qrCode)
        {
            try
            {
                using (var context = new vlEntities())
                {
                    var res = context.copies.Where(c => c.Id == qrCode).FirstOrDefault();
                    res.UserId = userId;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
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

        public List<books> GetUsersBooks(int id)
        {
            using (var context = new vlEntities())
            {
                var res = new List<books>();
                try
                {
                    var query =
                        from book in context.books
                        join copy in context.copies on book.Id equals copy.BookId
                        where copy.UserId == id
                        select new books();

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
    }
}
