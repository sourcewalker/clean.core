using Core.Shared.Utility;
using Infrastructure.ProCampaign.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ProCampaign.Helper
{
    public static class ApiHelper
    {
        static HttpClientHandler httpHandler = new HttpClientHandler();

        public static async Task<ProCampaignResponse> PostParticipationAsync(
            string participationData,
            ProCampaignSettings settings,
            bool requestConsumerId = false)
        {

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "ProCampaign settings cannot be null");
            }

            var apiUrl = new Uri(settings.ConsumerBaseUrl, settings.ParticipationPath);

            using (var client = new HttpClient(httpHandler, false))
            {
                client.BaseAddress = apiUrl;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                if (requestConsumerId)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        $"{StringUtility.Base64Encode($"{settings.ApiKey}:{settings.ApiSecret}")}");
                }

                string query;
                var queryStrings = requestConsumerId ?
                    new Dictionary<string, string>
                    {
                        { "apiKey", settings.ApiKey },
                        { "requestConsumerId", "true" }
                    } :
                    new Dictionary<string, string>
                    {
                        { "apiKey", settings.ApiKey }
                    };

                using (var content = new FormUrlEncodedContent(queryStrings))
                {
                    query = content.ReadAsStringAsync().Result;
                }

                var data = new StringContent(participationData, Encoding.UTF8, "application/json");
                string result;

                using (var response = await client.PostAsync($"{apiUrl}?{query}", data))
                {
                    using (var content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }

                return JsonConvert.DeserializeObject<ProCampaignResponse>(result);
            }
        }

        public static async Task<ProCampaignResponse> GetPermissionTextAsync(ProCampaignSettings settings)
        {

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "ProCampaign settings cannot be null");
            }

            var apiUrl = new Uri(settings.ApiBaseUrl, settings.DocumentPath);

            using (var client = new HttpClient(httpHandler, false))
            {
                client.BaseAddress = apiUrl;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string query;
                using (var content = new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "apiKey", settings.InternationalApiKey }
                    }))
                {
                    query = content.ReadAsStringAsync().Result;
                }

                string result;

                using (var response = await client.GetAsync($"{apiUrl}?{query}"))
                {
                    using (var content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }

                return JsonConvert.DeserializeObject<ProCampaignResponse>(result);
            }
        }
    }
}
