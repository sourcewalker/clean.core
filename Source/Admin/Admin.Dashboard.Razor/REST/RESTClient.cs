using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Admin.Dashboard.Razor.Client;
using Admin.Dashboard.Razor.ViewModels;

namespace Admin.Dashboard.Razor.REST
{
    public class RESTClient : IServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RESTClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<SiteViewModel>> GetAllSitesAsync()
        {
            var client = _httpClientFactory.CreateClient("RestClient");
            var result = await client.GetAsync("/configuration/getsites");

            var response = await result.Content.ReadAsAsync<ApiResponse>();

            return JsonConvert.DeserializeObject<List<SiteViewModel>>(response.Data.ToString());
        }

        public async Task<SiteViewModel> GetSiteByCultureAsync(string culture)
        {
            var client = _httpClientFactory.CreateClient("RestClient");
            var result = await client.GetAsync("/configuration/getsitebyculture");

            var response = await result.Content.ReadAsAsync<ApiResponse>();

            return JsonConvert.DeserializeObject<SiteViewModel>(response.Data.ToString());
        }
    }
}
