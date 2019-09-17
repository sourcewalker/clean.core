using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared.DTO;
using Core.Model;
using Infrastructure.DAL.EF.Repository.Base;
using Core.Infrastructure.Interfaces.DAL;
using Core.Infrastructure.Interfaces.Mapping;

namespace Infrastructure.DAL.EF.Repository.Implementations
{
    public class ParticipationRepository : RepositoryBase<Participation>, IParticipationRepository
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMappingProvider _mapper;

        public ParticipationRepository(IParticipantRepository participantRepository, IMappingProvider mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public IEnumerable<ParticipationDto> GetAll()
        {
            return _mapper.toDtos<ParticipationDto>(Table);
        }

        public int GetCount()
        {
            return Count;
        }

        public int GetCountBySite(Guid siteId)
        {
            return Context.ParticipationsQueryable
                    .Where(v => v.SiteId == siteId)
                    .Count();
        }

        public async Task<IEnumerable<ParticipationDto>> GetAllAsync()
            => await Task.Run(() => _mapper.toDtos<ParticipationDto>(Table));

        public ParticipationDto GetById(Guid id)
            => _mapper.toDto<ParticipationDto>(Find(id));

        public async Task<ParticipationDto> GetByIdAsync(Guid id)
            => await Task.Run(() => _mapper.toDto<ParticipationDto>(Find(id)));

        public IEnumerable<ParticipationDto> GetPaged(int pageNumber, int pageSize)
            => _mapper.toDtos<ParticipationDto>(GetRange(pageSize * (pageNumber - 1), pageSize));

        public async Task<IEnumerable<ParticipationDto>> GetPagedAsync(int pageNumber, int pageSize)
            => await Task.Run(() => _mapper.toDtos<ParticipationDto>(GetRange(pageSize * (pageNumber - 1), pageSize)));

        public bool Add(ParticipationDto participation)
        {
            participation.Id = participation.Id != default ? participation.Id : Guid.NewGuid();
            participation.CreatedDate = DateTimeOffset.UtcNow;
            return Add(_mapper.toEntity<Participation>(participation), true) > 0;
        }

        public async Task<bool> AddAsync(ParticipationDto participation)
        {
            participation.Id = participation.Id != default ? participation.Id : Guid.NewGuid();
            participation.CreatedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Add(_mapper.toEntity<Participation>(participation), true) > 0);
        }

        public bool Update(ParticipationDto participation)
        {
            var participationEntity = Find(participation.Id);
            participationEntity.Status = participation.Status;
            participationEntity.ApiStatus = participation.ApiStatus;
            participationEntity.ApiMessage = participation.ApiMessage;
            participationEntity.ModifiedDate = DateTimeOffset.UtcNow;
            return Update(participationEntity, true) > 0;
        }

        public async Task<bool> UpdateAsync(ParticipationDto participation)
        {
            participation.ModifiedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Update(_mapper.toEntity<Participation>(participation), true) > 0);
        }

        public bool Delete(ParticipationDto participation)
            => Delete(_mapper.toEntity<Participation>(participation), true) > 0;

        public async Task<bool> DeleteAsync(ParticipationDto participation)
            => await Task.Run(() => Delete(_mapper.toEntity<Participation>(participation), true) > 0);

        public bool Delete(Guid id)
        {
            var participation = GetById(id);
            return Delete(_mapper.toEntity<Participation>(participation), true) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
            => await Task.Run(() =>
            {
                var participation = GetById(id);
                return Delete(_mapper.toEntity<Participation>(participation), true) > 0;
            });

        public IEnumerable<ParticipationDto> GetPagedBySite(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParticipationDto>> GetPagedBySiteAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParticipationDto> GetPagedBySite(Guid siteId, int pageNumber, int pageSize)
        {
            IQueryable<Participation> filtered = Context.ParticipationsQueryable.Where(x => x.SiteId == siteId);
            return _mapper.toDtos<ParticipationDto>(GetRange(filtered, pageSize * (pageNumber - 1), pageSize));
        }

        public async Task<IEnumerable<ParticipationDto>> GetPagedBySiteAsync(Guid siteId, int pageNumber, int pageSize)
        {
            return await Task.Run((Func<IEnumerable<ParticipationDto>>)(() =>
            {
                IQueryable<Participation> filtered = Context.ParticipationsQueryable.Where(x => x.SiteId == siteId);
                return _mapper.toDtos<ParticipationDto>(GetRange((IQueryable<Participation>)filtered, 
                    (int)(pageSize * (pageNumber - 1)), (int)pageSize));
            }));
        }

        public IEnumerable<ParticipationDto> GetBySite(Guid siteId)
        {
            var participationBySite = Context.ParticipationsQueryable
                            .Where(x => x.SiteId == siteId)
                            .AsEnumerable<Participation>();
            return _mapper.toDtos<ParticipationDto>(participationBySite);
        }

        public async Task<IEnumerable<ParticipationDto>> GetBySiteAsync(Guid siteId)
        {
            return await Task.Run((Func<IEnumerable<ParticipationDto>>)(() =>
            {
                return _mapper.toDtos<ParticipationDto>(Context.ParticipationsQueryable
                            .Where((System.Linq.Expressions.Expression<Func<Participation, bool>>)(x => (bool)(x.SiteId == siteId)))
                            .AsEnumerable()
);
            }));
        }

        public ParticipationDto GetByEmailHash(string emailHash)
        {
            var participant = _participantRepository.GetByEmailHash(emailHash);
            var participation = Context.ParticipationsQueryable
                            .FirstOrDefault(x => x.Id == participant.ParticipationId);

            return _mapper.toDto<ParticipationDto>(participation);
        }

        public IEnumerable<ParticipationDto> GetBetween(DateTimeOffset start, DateTimeOffset end)
        {
            var between = Context.ParticipationsQueryable
                        .Where(v => start < v.CreatedDate && v.CreatedDate < end)
                        .AsEnumerable();
            return _mapper.toDtos<ParticipationDto>(between);
        }
    }
}
