using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Interfaces.DAL
{
    public interface ISiteRepository
    {
        IEnumerable<SiteDto> GetAll();

        Task<IEnumerable<SiteDto>> GetAllAsync();

        SiteDto GetById(Guid id);

        SiteDto GetByDomain(string domain);

        SiteDto GetByCulture(string culture);

        Task<SiteDto> GetByIdAsync(Guid id);

        IEnumerable<SiteDto> GetPaged(int pageNumber, int pageSize);

        Task<IEnumerable<SiteDto>> GetPagedAsync(int pageNumber, int pageSize);

        bool Add(SiteDto site);

        Task<bool> AddAsync(SiteDto site);

        bool Update(SiteDto site);

        Task<bool> UpdateAsync(SiteDto site);

        bool Delete(Guid id);

        Task<bool> DeleteAsync(Guid id);
    }
}
