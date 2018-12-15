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
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("login/byargs")]
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
