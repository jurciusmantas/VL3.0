using System.Collections.Generic;
using System.Web.Http;
using VirtualLibAPI.Models.Entities;
using VirtualLibAPI.Services;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IBookService _service;
        public BooksController(IBookService service)
        {
            _service = service;
        }
        public List<Book2> Get()
        {
            return _service.GetAllBooks();
        }
        public BookResponse Post([FromBody]BookQRCode book)
        {
            if (book.IsTaking)
            {
                return _service.Take(book.UserId, int.Parse(book.QRCode));
    
            }
            else
            {
                return new BookResponse
                {
                    WasUpdated = _service.Return(int.Parse(book.QRCode)),
                };
            }
        }

    }
}
