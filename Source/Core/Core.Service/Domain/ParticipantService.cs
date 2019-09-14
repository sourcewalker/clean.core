using Core.Infrastructure.Interfaces.DAL;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Core.Service.Domain
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IFailedTransactionRepository _failedRepository;

        public ParticipantService
        (
            IParticipantRepository participantRepository,
            IFailedTransactionRepository failedRepository
        )
        {
            _participantRepository = participantRepository;
            _failedRepository = failedRepository;
        }

        public bool CreateParticipant(ParticipantDto participant)
        {
            return _participantRepository.Add(participant);
        }

        public bool DeleteFailedParticipation(Guid id)
        {
            return _failedRepository.Delete(id);
        }

        public IEnumerable<ParticipantDto> GetAll()
        {
            return _participantRepository.GetAll();
        }

        public IEnumerable<ParticipantDto> GetBetween(DateTime start, DateTime end)
        {
            return _participantRepository.GetBetween(start, end);
        }

        public int GetTotalParticipantNumber()
        {
            return _participantRepository.GetCount();
        }

        public int GetTotalParticipantNumberBySite(Guid siteId)
        {
            return _participantRepository.GetCountBySite(siteId);
        }

        public ParticipantDto GetParticipant(Guid id)
        {
            return _participantRepository.GetById(id);
        }

        public IEnumerable<ParticipantDto> GetParticipantsBySite(Guid siteId)
        {
            return _participantRepository.GetBySite(siteId);
        }

        public IEnumerable<ParticipantDto> GetParticipantsPagedBySite(Guid siteId, int pageNumber, int pageSize)
        {
            return _participantRepository.GetPagedBySite(siteId, pageNumber, pageSize);
        }

        public bool UpdateParticipant(ParticipantDto participant)
        {
            return _participantRepository.Update(participant);
        }
    }
}
