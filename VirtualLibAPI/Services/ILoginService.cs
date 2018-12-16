using VirtualLibrarity.EFModel;

namespace VirtualLibAPI.Services
{
    public interface ILoginService
    {
        users ManualLogin(string email, string password);

        users FaceRecognitionLogin(int id);
    }
}