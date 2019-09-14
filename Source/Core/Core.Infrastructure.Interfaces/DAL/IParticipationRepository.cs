using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Interfaces.DAL
{
    public interface IParticipationRepository
    {
        IEnumerable<ParticipationDto> GetAll();

        IEnumerable<ParticipationDto> GetBetween(DateTimeOffset start, DateTimeOffset end);

        int GetCount();

        int GetCountBySite(Guid siteId);

        Task<IEnumerable<ParticipationDto>> GetAllAsync();

        ParticipationDto GetById(Guid id);

        Task<ParticipationDto> GetByIdAsync(Guid id);

        IEnumerable<ParticipationDto> GetPaged(int pageNumber, int pageSize);

        Task<IEnumerable<ParticipationDto>> GetPagedAsync(int pageNumber, int pageSize);

        IEnumerable<ParticipationDto> GetPagedBySite(Guid siteId, int pageNumber, int pageSize);

        Task<IEnumerable<ParticipationDto>> GetPagedBySiteAsync(Guid siteId, int pageNumber, int pageSize);

        IEnumerable<ParticipationDto> GetBySite(Guid siteId);

        Task<IEnumerable<ParticipationDto>> GetBySiteAsync(Guid siteId);

        bool Add(ParticipationDto participation);

        Task<bool> AddAsync(ParticipationDto participation);

        bool Update(ParticipationDto participation);

        Task<bool> UpdateAsync(ParticipationDto participation);

        bool Delete(Guid id);

        Task<bool> DeleteAsync(Guid id);

        ParticipationDto GetByEmailHash(string emailHash);
    }
}
