using System.Web.Http;
using VirtualLibAPI.Models.Entities;
using VirtualLibAPI.Services;

namespace VirtualLibrarity.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private readonly ILoginService _service;
        public LoginController(ILoginService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("byargs")]
        public UserToLoginResponse GetUserInfoByArgs([FromBody]LoginManualArgs loginManualArgs)
        {
            return new UserToLoginResponse
            {
                UserInfo = _service.ManualLogin(loginManualArgs.Email, loginManualArgs.Password),
                // BorrowedBooks
            };
        }
    }
}
