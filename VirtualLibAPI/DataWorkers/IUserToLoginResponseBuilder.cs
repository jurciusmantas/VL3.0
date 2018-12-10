using VirtualLibrarity.Models;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.DataWorkers
{
    public interface IUserToLoginResponseBuilder
    {
        UserToLoginResponse BuildUserToSend(int id);

        UserToLoginResponse BuildUserToSend(LoginManualArgs loginManualArgs);
    }
}