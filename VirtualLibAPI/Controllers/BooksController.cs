using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Models;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Controllers
{
    public class BooksController : ApiController
    {
        private BookRecognitionHandler bookRecognitionHandler = new BookRecognitionHandler();
        // POST: api/Books
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
