using Core.Shared.DTO;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DAL.EF.Repository.Base;
using Core.Infrastructure.Interfaces.DAL;
using Core.Infrastructure.Interfaces.Mapping;

namespace Infrastructure.DAL.EF.Repository.Implementations
{
    public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
    {
        private readonly IMappingProvider _mapper;

        public ParticipantRepository(IMappingProvider mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<ParticipantDto> GetAll()
        {
            return _mapper.toDtos<ParticipantDto>(Table);
        }

        public int GetCount()
        {
            return Count;
        }

        public int GetCountBySite(Guid siteId)
        {
            return Context.ParticipantsQueryable
                    .Join(Context.ParticipationsQueryable,
                          participant => participant.ParticipationId,
                          participation => participation.Id,
                          (participant, participation) => 
                            new {
                                Participant = participant,
                                Participation = participation
                            })
                    .Where(participationWithParticipant => 
                                participationWithParticipant.Participation.SiteId == siteId)
                    .Count();
        }

        public async Task<IEnumerable<ParticipantDto>> GetAllAsync()
            => await Task.Run(() => _mapper.toDtos<ParticipantDto>(Table));

        public ParticipantDto GetById(Guid id)
            => _mapper.toDto<ParticipantDto>(Find(id));

        public async Task<ParticipantDto> GetByIdAsync(Guid id)
            => await Task.Run(() => _mapper.toDto<ParticipantDto>(Find(id)));

        public IEnumerable<ParticipantDto> GetPaged(int pageNumber, int pageSize)
            => _mapper.toDtos<ParticipantDto>(GetRange(pageSize * (pageNumber - 1), pageSize));

        public async Task<IEnumerable<ParticipantDto>> GetPagedAsync(int pageNumber, int pageSize)
            => await Task.Run(() => _mapper.toDtos<ParticipantDto>(GetRange(pageSize * (pageNumber - 1), pageSize)));

        public bool Add(ParticipantDto participant)
        {
            participant.Id = participant.Id != default ? participant.Id : Guid.NewGuid();
            participant.CreatedDate = DateTimeOffset.UtcNow;
            return Add(_mapper.toEntity<Participant>(participant), true) > 0;
        }

        public async Task<bool> AddAsync(ParticipantDto vote)
        {
            vote.Id = vote.Id != default ? vote.Id : Guid.NewGuid();
            vote.CreatedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Add(_mapper.toEntity<Participant>(vote), true) > 0);
        }

        public bool Update(ParticipantDto vote)
        {
            var voteEntity = Find(vote.Id);
            voteEntity.EmailHash = vote.EmailHash;
            voteEntity.ConsumerId = vote.ConsumerId;
            voteEntity.ModifiedDate = DateTimeOffset.UtcNow;
            return Update(voteEntity, true) > 0;
        }

        public async Task<bool> UpdateAsync(ParticipantDto vote)
        {
            vote.ModifiedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Update(_mapper.toEntity<Participant>(vote), true) > 0);
        }

        public bool Delete(ParticipantDto vote)
            => Delete(_mapper.toEntity<Participant>(vote), true) > 0;

        public async Task<bool> DeleteAsync(ParticipantDto vote)
            => await Task.Run(() => Delete(_mapper.toEntity<Participant>(vote), true) > 0);

        public bool Delete(Guid id)
        {
            var vote = GetById(id);
            return Delete(_mapper.toEntity<Participant>(vote), true) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
            => await Task.Run(() =>
            {
                var vote = GetById(id);
                return Delete(_mapper.toEntity<Participant>(vote), true) > 0;
            });

        public IEnumerable<ParticipantDto> GetPagedBySite(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParticipantDto>> GetPagedBySiteAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParticipantDto> GetPagedBySite(Guid siteId, int pageNumber, int pageSize)
        {
            IQueryable<Participant> filtered = 
                Context.ParticipantsQueryable
                .Join(Context.ParticipationsQueryable,
                          participant => participant.ParticipationId,
                          participation => participation.Id,
                          (participant, participation) =>
                            new {
                                Participant = participant,
                                Participation = participation
                            })
                    .Where(participationWithParticipant =>
                                participationWithParticipant.Participation.SiteId == siteId)
                    .Select(participationWithParticipant => participationWithParticipant.Participant);
            return _mapper.toDtos<ParticipantDto>(GetRange(filtered, pageSize * (pageNumber - 1), pageSize));
        }

        public async Task<IEnumerable<ParticipantDto>> GetPagedBySiteAsync(Guid siteId, int pageNumber, int pageSize)
        {
            return await Task.Run(() =>
            {
                IQueryable<Participant> filtered =
                    Context.ParticipantsQueryable
                    .Join(Context.ParticipationsQueryable,
                              participant => participant.ParticipationId,
                              participation => participation.Id,
                              (participant, participation) =>
                                new {
                                    Participant = participant,
                                    Participation = participation
                                })
                        .Where(participationWithParticipant =>
                                    participationWithParticipant.Participation.SiteId == siteId)
                        .Select(participationWithParticipant => participationWithParticipant.Participant);
                return _mapper.toDtos<ParticipantDto>(GetRange(filtered, pageSize * (pageNumber - 1), pageSize));
            });
        }

        public IEnumerable<ParticipantDto> GetBySite(Guid siteId)
        {
            var participantsBySite = Context.ParticipantsQueryable
                    .Join(Context.ParticipationsQueryable,
                              participant => participant.ParticipationId,
                              participation => participation.Id,
                              (participant, participation) =>
                                new {
                                    Participant = participant,
                                    Participation = participation
                                })
                        .Where(participationWithParticipant =>
                                    participationWithParticipant.Participation.SiteId == siteId)
                        .Select(participationWithParticipant => participationWithParticipant.Participant)
                        .AsEnumerable();
            return _mapper.toDtos<ParticipantDto>(participantsBySite);
        }

        public async Task<IEnumerable<ParticipantDto>> GetBySiteAsync(Guid siteId)
        {
            return await Task.Run(() =>
            {
                var participantsBySite = Context.ParticipantsQueryable
                    .Join(Context.ParticipationsQueryable,
                              participant => participant.ParticipationId,
                              participation => participation.Id,
                              (participant, participation) =>
                                new {
                                    Participant = participant,
                                    Participation = participation
                                })
                        .Where(participationWithParticipant =>
                                    participationWithParticipant.Participation.SiteId == siteId)
                        .Select(participationWithParticipant => participationWithParticipant.Participant)
                        .AsEnumerable();
                return _mapper.toDtos<ParticipantDto>(participantsBySite);
            });
        }

        public ParticipantDto GetByEmailHash(string emailHash)
        {
            var vote = Context.ParticipantsQueryable
                            .FirstOrDefault(x => x.EmailHash == emailHash);

            return _mapper.toDto<ParticipantDto>(vote);
        }

        public IEnumerable<ParticipantDto> GetBetween(DateTimeOffset start, DateTimeOffset end)
        {
            var between = Context.ParticipantsQueryable
                        .Where(v => start < v.CreatedDate && v.CreatedDate < end)
                        .AsEnumerable();
            return _mapper.toDtos<ParticipantDto>(between);
        }
    }
}
