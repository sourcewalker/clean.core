using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.Site.MVC.Client;
using Web.Site.MVC.ViewModels;

namespace Web.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceClient _client;

        public HomeController(IServiceClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = requestCulture.RequestCulture.Culture.Name;
            var site = await _client.GetSiteByCultureAsync(culture);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
