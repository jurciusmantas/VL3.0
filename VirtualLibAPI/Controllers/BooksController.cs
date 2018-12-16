using System.Web.Http;
using VirtualLibAPI.Models.Entities;
using VirtualLibAPI.Services;
using VirtualLibrarity.DataWorkers;

namespace VirtualLibrarity.Controllers
{
    public class BooksController : ApiController
    {

        public BooksAndCategories Get()
        {
            return new BooksAndCategoriesBuilder().CreateBooksAndCategoriesList();
        }
        public BookResponse Post([FromBody]BookQRCode book)
        {
            if (book.IsTaking)
            {
                return new BookResponse
                {
                    //BookInfo = MigrationResolver.TakeBook(book.UserId, int.Parse(book.QRCode)),
                };
            }
            else
            {
                return new BookResponse
                {
                    WasUpdated = MigrationResolver.ReturnBook(int.Parse(book.QRCode)),
                };
            }
        }

    }
}
