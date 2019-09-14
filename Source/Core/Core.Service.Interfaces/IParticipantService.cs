using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Core.Service.Interfaces
{
    public interface IParticipantService
    {
        IEnumerable<ParticipantDto> GetAll();

        IEnumerable<ParticipantDto> GetBetween(DateTime start, DateTime end);

        IEnumerable<ParticipantDto> GetParticipantsPagedBySite(Guid siteId, int pageNumber, int pageSize);

        IEnumerable<ParticipantDto> GetParticipantsBySite(Guid siteId);

        ParticipantDto GetParticipant(Guid id);

        int GetTotalParticipantNumberBySite(Guid siteId);

        int GetTotalParticipantNumber();

        bool CreateParticipant(ParticipantDto vote);

        bool UpdateParticipant(ParticipantDto vote);

        bool DeleteFailedParticipation(Guid id);
    }
}
