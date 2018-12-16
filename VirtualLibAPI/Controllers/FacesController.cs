using System.Web.Http;
using VirtualLibAPI.Models.Entities;
using VirtualLibAPI.Services;
using VirtualLibDatabase.Entities;

namespace VirtualLibAPI.Controllers
{
    public class FacesController : ApiController
    {
        private IPostHandler _ph;
       public FacesController()
        {
            _ph = new PostHandler(new FileFaceReader(), new FileFaceWriter(), new APIRecognizer(new FacePlusRequest()), new RegisterService(), new LoginService());
        }
       

        // POST api/faces
        public UserToLoginResponse Post([FromBody]Face face)
        {
            return _ph.HandlePost(face);
        }

    }
}
