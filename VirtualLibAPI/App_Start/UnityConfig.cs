using System.Web.Http;
using Unity;
using Unity.WebApi;
using VirtualLibAPI;
using VirtualLibrarity.DataWorkers;

namespace VirtualLibrarity
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IPostHandler, PostHandler>();
            container.RegisterType<IReader, FileFaceReader>();
            container.RegisterType<IWriter, FileFaceWriter>();
            container.RegisterType<IRequest, FacePlusRequest>();
            container.RegisterType<IRecognizer, APIRecognizer>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}