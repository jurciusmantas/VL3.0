using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Services
{
    public interface IRegisterService
    {
        bool Register(users user);
    }
}