﻿using System.Web.Http;
using VirtualLibAPI.Models.Entities;
using VirtualLibAPI.Services;

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
