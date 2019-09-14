using Core.Shared.Models;

namespace Core.Infrastructure.Interfaces.UserData
{
    public interface IUserDataProvider
    {
        Configurations Configuration { get; set; }

        User GetUserDetails<T>(T id);
    }
}
