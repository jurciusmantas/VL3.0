using Autofac;
using System.Web.Http;
using VirtualLibDatabase.Entities;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Models;

namespace VirtualLibAPI.Controllers
{
    public class FacesController : ApiController
    {
        private IPostHandler _ph;
       public FacesController()
        {
            _ph = new PostHandler(new FileFaceReader(), new FileFaceWriter(), new APIRecognizer(new FacePlusRequest()), new RegisterDataHandler());
        }
       

        // POST api/faces
        public UserToLoginResponse Post([FromBody]Face face)
        {
            return _ph.HandlePost(face);
        }

    }
}
