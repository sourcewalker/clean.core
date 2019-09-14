using Core.Infrastructure.Interfaces.DAL;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Core.Service.Domain
{
    public class ParticipationService : IParticipationService
    {
        private readonly IParticipationRepository _participationRepository;

        public ParticipationService
        (
            IParticipationRepository participationRepository
        )
        {
            _participationRepository = participationRepository;
        }

        public bool CreateParticipation(ParticipationDto participation)
        {
            return _participationRepository.Add(participation);
        }

        public IEnumerable<ParticipationDto> GetAll()
        {
            return _participationRepository.GetAll();
        }

        public IEnumerable<ParticipationDto> GetBetween(DateTimeOffset start, DateTimeOffset end)
        {
            return _participationRepository.GetBetween(start, end);
        }

        public int GetTotalParticipationNumber()
        {
            return _participationRepository.GetCount();
        }

        public int GetTotalParticipationNumberBySite(Guid siteId)
        {
            return _participationRepository.GetCountBySite(siteId);
        }

        public ParticipationDto GetParticipation(Guid id)
        {
            return _participationRepository.GetById(id);
        }

        public IEnumerable<ParticipationDto> GetParticipationsBySite(Guid siteId)
        {
            return _participationRepository.GetBySite(siteId);
        }

        public IEnumerable<ParticipationDto> GetParticipationsPagedBySite(Guid siteId, int pageNumber, int pageSize)
        {
            return _participationRepository.GetPagedBySite(siteId, pageNumber, pageSize);
        }

        public bool UpdateParticipation(ParticipationDto participation)
        {
            return _participationRepository.Update(participation);
        }
    }
}
