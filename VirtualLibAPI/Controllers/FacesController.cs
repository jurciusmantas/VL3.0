using Autofac;
using System.Web.Http;
using VirtualLibrarity.Models;

namespace VirtualLibAPI.Controllers
{
    public class FacesController : ApiController
    {
        private IPostHandler _ph;
       public FacesController()
        {
            _ph = WebApiApplication.container.Resolve<IPostHandler>();
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
