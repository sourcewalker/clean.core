using Core.Infrastructure.Interfaces.CRM;
using Core.Shared.Models;
using Infrastructure.ProCampaign.Constant;
using Infrastructure.ProCampaign.Helper;
using Infrastructure.ProCampaign.Models;
using System;
using System.Threading.Tasks;

namespace Infrastructure.ProCampaign.Consumer
{
    public class ConsumerProvider : ICrmConsumerProvider
    {
        public Configurations Configuration { get; set; }

        public ConsumerProvider()
        {
            Configuration = new Configurations();

            // Consumer endpoint configuration
            Configuration.Settings.ConsumerBaseUrl = ProCampaignConstants.ConsumerAPI.BaseUrl;
            Configuration.Settings.ParticipationPath = ProCampaignConstants.ConsumerAPI.ParticipationPath;

            // API endpoint configuration
            Configuration.Settings.DocumentBaseUrl = ProCampaignConstants.DocumentAPI.BaseUrl;
            Configuration.Settings.DocumentPath = $"{ProCampaignConstants.DocumentAPI.DocumentPath}/" +
                                                  $"{ProCampaignConstants.DocumentAPI.DefaultListName}/" +
                                                  $"{ProCampaignConstants.DocumentAPI.DefaultDocumentName}";
            Configuration.Settings.InternationalApiKey = $"{ProCampaignConstants.DocumentAPI.InternationalApiKey}";
        }

        public async Task<CrmData> CreateParticipationAsync(
            CrmData data,
            Configurations requestWideSettings,
            bool requestConsumerId = false)
        {
            data.AddSetting("SourceName", requestWideSettings.Settings.Source);
            data.AddSetting("TransactionName", requestWideSettings.Settings.Transaction);

            data.AddSetting("ListPrivacyPolicy", (bool)data.Data.PrivacyConsent ? 1 : 0);
            data.AddSetting("ListCadbury", (bool)data.Data.NewsletterOptin ? 1 : 0);
            data.AddSetting("IdentLong", (string)data.Data.Retailer);
            data.AddSetting("IdentShort", (int)data.Data.ChocolateBar);
            data.AddSetting("Q1", (string)data.Data.Place);
            data.AddSetting("Q2", (string)data.Data.TriedFlavours);
            data.AddSetting("PrivacyPolicyTextName", (string)data.Data.PrivacyPolicyTextName);
            data.AddSetting("PrivacyPolicyVersion", (long)data.Data.PrivacyPolicyVersion);
            data.AddSetting("PrivacyPolicyCreation", (string)data.Data.PrivacyPolicyCreation);

            var settings = new ProCampaignSettings
            {
                ConsumerBaseUrl = new Uri(Configuration.Settings.ConsumerBaseUrl),
                ParticipationPath = Configuration.Settings.ParticipationPath,
                ApiKey = requestWideSettings.Settings.ApiKey,
                ApiSecret = requestWideSettings.Settings.ApiSecret
            };
            var ApiData = ProCampaignData.FormatParticipationData(data);

            var response = await ApiHelper.PostParticipationAsync(ApiData, settings, requestConsumerId);

            var returnData = new CrmData();
            returnData.AddSetting("Success", response.IsSuccessful);
            returnData.AddSetting("ApiStatus", response.StatusCode);
            returnData.AddSetting("ApiMessage", response.StatusMessage);
            returnData.AddSetting("HttpStatus", response.HttpStatusCode);
            returnData.AddSetting("HttpMessage", response.HttpStatusMessage);
            returnData.AddSetting("Data", response.Data);

            return returnData;
        }

        public async Task<CrmData> ReadTextDocumentAsync()
        {
            var settings = new ProCampaignSettings
            {
                ApiBaseUrl = new Uri(Configuration.Settings.DocumentBaseUrl),
                DocumentPath = Configuration.Settings.DocumentPath,
                InternationalApiKey = Configuration.Settings.InternationalApiKey
            };

            var response = await ApiHelper.GetPermissionTextAsync(settings);

            var returnData = new CrmData();
            returnData.AddSetting("Success", response.IsSuccessful);
            returnData.AddSetting("ApiStatus", response.StatusCode);
            returnData.AddSetting("ApiMessage", response.StatusMessage);
            returnData.AddSetting("HttpStatus", response.HttpStatusCode);
            returnData.AddSetting("HttpMessage", response.HttpStatusMessage);
            returnData.AddSetting("Data", response.Data);

            return returnData;
        }
    }
}
