using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Interfaces
{
    public interface IParticipationService
    {
        IEnumerable<ParticipationDto> GetAll();

        IEnumerable<ParticipationDto> GetBetween(DateTimeOffset start, DateTimeOffset end);

        IEnumerable<ParticipationDto> GetParticipationsPagedBySite(Guid siteId, int pageNumber, int pageSize);

        IEnumerable<ParticipationDto> GetParticipationsBySite(Guid siteId);

        ParticipationDto GetParticipation(Guid id);

        int GetTotalParticipationNumberBySite(Guid siteId);

        int GetTotalParticipationNumber();

        bool CreateParticipation(ParticipationDto vote);

        bool UpdateParticipation(ParticipationDto vote);
    }
}
