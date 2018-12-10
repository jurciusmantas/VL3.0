using System.Threading.Tasks;
using VirtualLibrarity.Interfaces;

namespace VirtualLibAPI
{
    public interface IRequest
    {
      T MakeRequestAsync<T>(string image1, string image2)
            where T:IResponse;
    }
}
