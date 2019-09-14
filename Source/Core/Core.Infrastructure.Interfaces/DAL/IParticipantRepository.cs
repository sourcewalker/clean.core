using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Interfaces.DAL
{
    public interface IParticipantRepository
    {
        IEnumerable<ParticipantDto> GetAll();

        IEnumerable<ParticipantDto> GetBetween(DateTimeOffset start, DateTimeOffset end);

        int GetCount();

        int GetCountBySite(Guid siteId);

        Task<IEnumerable<ParticipantDto>> GetAllAsync();

        ParticipantDto GetById(Guid id);

        Task<ParticipantDto> GetByIdAsync(Guid id);

        IEnumerable<ParticipantDto> GetPaged(int pageNumber, int pageSize);

        Task<IEnumerable<ParticipantDto>> GetPagedAsync(int pageNumber, int pageSize);

        IEnumerable<ParticipantDto> GetPagedBySite(Guid siteId, int pageNumber, int pageSize);

        Task<IEnumerable<ParticipantDto>> GetPagedBySiteAsync(Guid siteId, int pageNumber, int pageSize);

        IEnumerable<ParticipantDto> GetBySite(Guid siteId);

        Task<IEnumerable<ParticipantDto>> GetBySiteAsync(Guid siteId);

        bool Add(ParticipantDto vote);

        Task<bool> AddAsync(ParticipantDto vote);

        bool Update(ParticipantDto vote);

        Task<bool> UpdateAsync(ParticipantDto vote);

        bool Delete(Guid id);

        Task<bool> DeleteAsync(Guid id);

        ParticipantDto GetByEmailHash(string emailHash);
    }
}
