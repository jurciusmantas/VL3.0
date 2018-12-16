using System.Web.Http;
using VirtualLibAPI;
using VirtualLibrarity.Models;
using VirtualLibrarity.Services;

namespace VirtualLibrarity.Controllers
{
    public class RegistrationController : ApiController
    {
        private readonly IPostHandler _postHandler;
        private readonly IDeleteService _deleteService;
        public RegistrationController(IPostHandler postHandler, IDeleteService deleteService)
        {
            _postHandler = postHandler;
            _deleteService = deleteService;
        }

        [HttpPost]
        [Route("register")]
        public int Post([FromBody] RegisterArgs regArgs)
        {
            return _postHandler.HandleRegisterPost(regArgs);
        }
        [HttpPost]
        [Route("delete")]
        public bool Delete([FromBody] int UserId)
        {
            return _deleteService.DeleteUser(UserId);
        }
    }
}
