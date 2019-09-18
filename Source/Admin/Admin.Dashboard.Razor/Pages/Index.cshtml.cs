using Admin.Dashboard.Razor.Client;
using Admin.Dashboard.Razor.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Dashboard.Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IServiceClient _client;

        public IndexModel(IServiceClient client)
        {
            _client = client;
        }

        public SiteViewModel Site { get; set; }

        public void OnGet()
        {
            Site = _client.GetSiteByCultureAsync("en-GB").Result;
        }
    }
}
