using Core.Infrastructure.Interfaces.DAL;
using Core.Infrastructure.Interfaces.Mapping;
using Core.Model;
using Core.Shared.DTO;
using Infrastructure.DAL.EF.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.DAL.EF.Repository.Implementations
{
    public class SiteRepository : RepositoryBase<Site>, ISiteRepository
    {
        private readonly IMappingProvider _mapper;

        public SiteRepository(IMappingProvider mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<SiteDto> GetAll()
            => _mapper.toDtos<SiteDto>(Table);

        public async Task<IEnumerable<SiteDto>> GetAllAsync()
            => await Task.Run(() => _mapper.toDtos<SiteDto>(Table));

        public SiteDto GetById(Guid id)
            => _mapper.toDto<SiteDto>(Find(id));

        public async Task<SiteDto> GetByIdAsync(Guid id)
            => await Task.Run(() => _mapper.toDto<SiteDto>(Find(id)));

        public IEnumerable<SiteDto> GetPaged(int pageNumber, int pageSize)
            => _mapper.toDtos<SiteDto>(GetRange(pageSize * (pageNumber - 1), pageSize));

        public async Task<IEnumerable<SiteDto>> GetPagedAsync(int pageNumber, int pageSize)
            => await Task.Run(() => _mapper.toDtos<SiteDto>(GetRange(pageSize * (pageNumber - 1), pageSize)));

        public bool Add(SiteDto site)
        {
            site.Id = site.Id != default ? site.Id : Guid.NewGuid();
            site.CreatedDate = DateTimeOffset.UtcNow;
            return Add(_mapper.toEntity<Site>(site), true) > 0;
        }

        public async Task<bool> AddAsync(SiteDto site)
        {
            site.Id = site.Id != default ? site.Id : Guid.NewGuid();
            site.CreatedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Add(_mapper.toEntity<Site>(site), true) > 0);
        }

        public bool Update(SiteDto site)
        {
            site.ModifiedDate = DateTimeOffset.UtcNow;
            return Update(_mapper.toEntity<Site>(site), true) > 0;
        }

        public async Task<bool> UpdateAsync(SiteDto site)
        {
            site.ModifiedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Update(_mapper.toEntity<Site>(site), true) > 0);
        }

        public bool Delete(SiteDto site)
            => Delete(_mapper.toEntity<Site>(site), true) > 0;

        public async Task<bool> DeleteAsync(SiteDto site)
            => await Task.Run(() => Delete(_mapper.toEntity<Site>(site), true) > 0);

        public bool Delete(Guid id)
        {
            var site = GetById(id);
            return Delete(_mapper.toEntity<Site>(site), true) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
            => await Task.Run(() =>
            {
                var site = GetById(id);
                return Delete(_mapper.toEntity<Site>(site), true) > 0;
            });

        public SiteDto GetByDomain(string domain)
        {
            var siteByDomain = Context.SitesQueryable
                            .First(x => domain.Contains(x.Domain));
            return _mapper.toDto<SiteDto>(siteByDomain);
        }

        public async Task<SiteDto> GetByDomainAsync(string domain)
            => await Task.Run(() =>
            {
                var siteByDomain = Context.SitesQueryable
                        .First(x => domain.Contains(x.Domain));
                return _mapper.toDto<SiteDto>(siteByDomain);
            });

        public SiteDto GetByCulture(string culture)
        {
            var siteByCulture = Context.SitesQueryable
                            .First(x => x.Culture == culture);
            return _mapper.toDto<SiteDto>(siteByCulture);
        }
    }
}
