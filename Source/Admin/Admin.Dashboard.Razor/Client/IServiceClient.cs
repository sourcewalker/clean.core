using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Dashboard.Razor.ViewModels;

namespace Admin.Dashboard.Razor.Client
{
    public interface IServiceClient
    {
        Task<List<SiteViewModel>> GetAllSitesAsync();

        Task<SiteViewModel> GetSiteByCultureAsync(string culture);

        Task<SiteViewModel> CreateSiteAsync(SiteViewModel siteToCreate);

        Task<SiteViewModel> UpdateSiteAsync(Guid id, SiteViewModel siteToUpdate);

        Task<string> DeleteSiteAsync(Guid id);
    }
}
