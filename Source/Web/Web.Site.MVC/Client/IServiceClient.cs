using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Site.MVC.ViewModels;

namespace Web.Site.MVC.Client
{
    public interface IServiceClient
    {
        Task<List<SiteViewModel>> GetAllSitesAsync();

        Task<SiteViewModel> GetSiteByCultureAsync(string culture);
    }
}
