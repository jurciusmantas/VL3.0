using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.DataWorkers
{
    public interface IRegisterDataHandler
    {
        bool Insert(User user, int id);
    }
}