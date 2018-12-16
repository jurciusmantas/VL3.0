namespace VirtualLibAPI.Services
{
    public interface IBookService
    {
        bool Take(int userId, int qrCode);

        bool Return(int qrCode);
    }
}