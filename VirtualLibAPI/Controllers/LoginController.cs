using System.Web.Http;
using VirtualLibAPI.Models.Entities;
using VirtualLibAPI.Services;

namespace VirtualLibrarity.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private readonly ILoginService _loginService;
        private readonly IBookService _bookService;
        public LoginController(ILoginService loginService, IBookService bookService)
        {
            _loginService = loginService;
            _bookService = bookService;
        }
        [HttpPost]
        [Route("byargs")]
        public UserToLoginResponse GetUserInfoByArgs([FromBody]LoginManualArgs loginManualArgs)
        {
            var user = _loginService.ManualLogin(loginManualArgs.Email, loginManualArgs.Password);
            var borrowedBooks = _bookService.GetUsersBorrowedBooks(user.Id);
            return new UserToLoginResponse
            {
                UserInfo = new Models.Entities.User {
                    Id = user.Id,
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    Email = user.Email,
                },
                BorrowedBooks = borrowedBooks,
            };
        }
    }
}
