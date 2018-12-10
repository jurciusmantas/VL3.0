using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirtualLibAPI;
using VirtualLibrarity.Models;

namespace VirtualLibrarity.Controllers
{
    public class RegistrationController : ApiController
    {
        private readonly IPostHandler _postHandler;
        public RegistrationController(IPostHandler postHandler)
        {
            _postHandler = postHandler;
        }

        [HttpPost]
        [Route("register")]
        public int Post([FromBody] RegisterArgs regArgs)
        {
            return _postHandler.HandleRegisterPost(regArgs);
        }

    }
}
