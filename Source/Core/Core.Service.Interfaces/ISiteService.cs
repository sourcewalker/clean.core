using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Interfaces
{
    public interface ISiteService
    {
        IEnumerable<SiteDto> GetSitesPaged(int pageNumber, int pageSize);

        SiteDto GetSite(Guid id);

        SiteDto GetSiteByDomain(string domain);

        SiteDto GetSiteByCulture(string culture);

        bool CreateSite(SiteDto site);

        bool UpdateSite(SiteDto site);

        bool DeleteSite(Guid id);
    }
}
