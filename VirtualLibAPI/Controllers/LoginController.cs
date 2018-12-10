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
    public class LoginController : ApiController
    {
        private readonly IUserToLoginResponseBuilder _responseBuilder;
        public LoginController(IUserToLoginResponseBuilder builder)
        {
            _responseBuilder = builder;
        }

        [HttpPost]
        [Route("login/byid")]
        public UserToLoginResponse GetUserInfoById([FromBody]int id)
        {
            return _responseBuilder.BuildUserToSend(id);
        }

        [HttpPost]
        [Route("login/byargs")]
        public UserToLoginResponse GetUserInfoByArgs([FromBody]LoginManualArgs loginManualArgs)
        {
            return _responseBuilder.BuildUserToSend(loginManualArgs);
        }
    }
}
