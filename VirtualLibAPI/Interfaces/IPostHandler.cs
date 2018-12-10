using VirtualLibrarity.Interfaces;
using VirtualLibrarity.Models;

namespace VirtualLibAPI
{
    public interface IPostHandler
    {
        UserToLoginResponse HandlePost<F>(F face)
            where F:IFace;
        int HandleRegisterPost(RegisterArgs regArgs);
    }
}
