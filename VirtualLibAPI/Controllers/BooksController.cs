using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualLibDatabase;
using VirtualLibDatabase.Entities;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Models;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Controllers
{
    public class BooksController : ApiController
    {
        private BookRecognitionHandler bookRecognitionHandler = new BookRecognitionHandler();

        public BooksAndCategories Get()
        {
            return new BooksAndCategoriesBuilder().CreateBooksAndCategoriesList();
        }
        public BookResponse Post([FromBody ]BookQRCode book)
        {
            if (book.IsTaking)
            {
                return bookRecognitionHandler.Take(book.QRCode, book.UserId);
            }
            else
            {
                return bookRecognitionHandler.Return(book.QRCode, book.UserId);
            }
        }

    }
}
