using Core.Shared.DTO;
using System.Threading.Tasks;

namespace Core.Service.Interfaces
{
    public interface IJourneyService
    {
        Task<(bool, string)> ParticipateAsync(ParticipationDto participation, string culture, string country = "GB");
    }
}
