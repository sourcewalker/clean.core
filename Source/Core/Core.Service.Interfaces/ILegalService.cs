using System.Threading.Tasks;

namespace Core.Service.Interfaces
{
    public interface ILegalService
    {
        Task<string> GetPrivacyPolicyTextAsync();
    }
}
