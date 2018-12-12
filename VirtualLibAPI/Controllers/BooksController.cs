using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Models;

namespace VirtualLibrarity.Controllers
{
    public class BooksController : ApiController
    {
        private BookRecognitionHandler bookRecognitionHandler = new BookRecognitionHandler();
        // POST: api/Books
        public bool Post([FromBody ]BookQRCode book)
        {
            if (book.IsTaking)
            {
                return bookRecognitionHandler.Take(book.QRCode);
            }
            else
            {
                return bookRecognitionHandler.Return(book.QRCode);
            }
        }

    }
}
