using Autofac;
using System.Web.Http;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Models;

namespace VirtualLibAPI.Controllers
{
    public class FacesController : ApiController
    {
        private IPostHandler _ph;
       public FacesController()
        {
            // _ph = WebApiApplication.container.Resolve<IPostHandler>();
            _ph = new PostHandler(new FileFaceReader(), new FileFaceWriter(), new APIRecognizer(new FacePlusRequest()), new RegisterDataHandler());
        }
       

        // POST api/faces
        public UserToLoginResponse Post([FromBody]Face face)
        {
            return _ph.HandlePost(face);
        }


        // DELETE api/faces/5
        public void Delete(int id)
        {
        }
    }
}
