using Infrastructure.Community.Models;

namespace Infrastructure.Community.Interfaces
{
    public interface IProfileProvider
    {
        KuhmunityResponse GetProfile();
    }
}
