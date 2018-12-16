using System.Web.Http;
using VirtualLibDatabase;
using VirtualLibDatabase.Entities;

namespace VirtualLibrarity.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("byargs")]
        public UserToLoginResponse GetUserInfoByArgs([FromBody]LoginManualArgs loginManualArgs)
        {
            return new UserToLoginResponse
            {
                UserInfo = MigrationResolver.Login(loginManualArgs.Email, loginManualArgs.Password),
                // BorrowedBooks
            };
        }
    }
}
