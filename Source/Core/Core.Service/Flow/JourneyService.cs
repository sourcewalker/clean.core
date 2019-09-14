using Core.Infrastructure.Interfaces.Configuration;
using Core.Infrastructure.Interfaces.CRM;
using Core.Infrastructure.Interfaces.Logging;
using Core.Infrastructure.Interfaces.Scheduler;
using Core.Service.Interfaces;
using Core.Service.Status;
using Core.Shared.DTO;
using Core.Shared.Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Core.Service.Flow
{
    public class JourneyService : IJourneyService
    {
        private readonly IParticipationService _participationService;
        private readonly IParticipantService _participantService;
        private readonly ILoggingProvider _logger;
        private readonly ICrmConsumerProvider _crmProvider;
        private readonly ISchedulerProvider _scheduler;
        private readonly IConfigurationProvider _configProvider;
        private readonly IFailedTransactionService _failedTransactionService;

        private string sourceConfigKey = "CSX:Consumer:SourceName";
        private string transactionConfigKey = "CSX:Consumer:Participation:TransactionName";
        private string apiKeyConfigKey = "CSX:Consumer:ApiKey";
        private string apiSecretConfigKey = "CSX:Consumer:ApiSecret";

        public JourneyService
        (
            IParticipationService participationService,
            IParticipantService participantService,
            ILoggingProvider logger,
            ICrmConsumerProvider crmProvider,
            ISchedulerProvider scheduler,
            IConfigurationProvider configProvider,
            IFailedTransactionService failedTransactionService
        )
        {
            _participationService = participationService;
            _participantService = participantService;
            _logger = logger;
            _crmProvider = crmProvider;
            _scheduler = scheduler;
            _configProvider = configProvider;
            _failedTransactionService = failedTransactionService;
        }

        public async Task<(bool, string)> ParticipateAsync(ParticipationDto participation, string culture, string country = "GB")
        {
            // Save Participation to DB
            participation.Status = ParticipationStatus.PARTICIPATION_NOT_SYNCED.ToString();
            participation.CreatedDate = DateTimeOffset.UtcNow;
            _participationService.CreateParticipation(participation);


            // Requesting for legal document
            var legalDocument = await _crmProvider.ReadTextDocumentAsync();

            var crmData = GatherParticipationCrmDataToSend(participation, legalDocument, country);
            var configuration = Configure(culture);

            // Sending Participation to Crm
            var consumer = await _crmProvider.CreateParticipationAsync(crmData, configuration, true);

            var success = consumer.GetSetting<bool>("Success");
            var ApiStatus = consumer.GetSetting<int>("ApiStatus");
            var ApiMessage = consumer.GetSetting<string>("ApiMessage");
            var consumerId = success ? (string)consumer.Data.Data.ConsumerId : string.Empty;

            if (success)
            {
                var createdParticipation = _participationService.GetParticipation(participation.Id);
                createdParticipation.Status = ParticipationStatus.PARTICIPATION_SYNCED_SUCCESS.ToString();
                createdParticipation.ApiStatus = ApiStatus.ToString();
                createdParticipation.ApiMessage = ApiMessage;
                createdParticipation.ConsumerId = consumerId;
                _participationService.UpdateParticipation(createdParticipation);
            }
            else
            {
                var createdParticipation = _participationService.GetParticipation(participation.Id);
                createdParticipation.Status = ParticipationStatus.PARTICIPATION_SYNCED_FAILED.ToString();
                createdParticipation.ApiStatus = ApiStatus.ToString();
                createdParticipation.ApiMessage = ApiMessage;
                _participationService.UpdateParticipation(createdParticipation);

                (success, consumerId) = await ProceedToParticipationSyncRetryProcess(participation, crmData, configuration);
            }
            return (success, consumerId);
        }

        private CrmData GatherParticipationCrmDataToSend(ParticipationDto participation, CrmData legalDocument, string country)
        {
            var crmData = new CrmData();

            crmData.Data.Email = participation.Email;
            crmData.Data.Country = country;
            crmData.Data.PrivacyConsent = true;
            crmData.Data.NewsletterOptin = participation.NewsletterOptin;

            crmData.Data.PrivacyPolicyTextName = legalDocument.Data.Data.Versions[0].LegalTextName;
            crmData.Data.PrivacyPolicyVersion = legalDocument.Data.Data.Versions[0].Version;
            crmData.Data.PrivacyPolicyCreation = legalDocument.Data.Data.Versions[0].Created.ToString();

            return crmData;
        }

        private Configurations Configure(string culture)
        {
            var configuration = new Configurations();
            configuration.Settings.Source = _configProvider.GetSharedConfig(sourceConfigKey);
            configuration.Settings.Transaction = _configProvider.GetSharedConfig(transactionConfigKey);
            configuration.Settings.ApiKey = _configProvider.GetConfigByCultureAndEnvironment(apiKeyConfigKey,
                                                CultureInfo.GetCultureInfo(culture));
            configuration.Settings.ApiSecret = _configProvider.GetConfigByCultureAndEnvironment(apiSecretConfigKey,
                                                    CultureInfo.GetCultureInfo(culture));
            return configuration;
        }

        private async Task<(bool, string)> ProceedToParticipationSyncRetryProcess(ParticipationDto participation, CrmData crmData, Configurations configuration)
        {
            var success = false;
            var consumerId = string.Empty;

            // Store locally
            var failed = new FailedTransactionDto
            {
                Id = Guid.NewGuid(),
                ParticipationId = participation.Id,
                TermsConsent = true,
                NewsletterOptin = participation.NewsletterOptin,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _failedTransactionService.Create(failed);

            // Another attempt
            var reSyncResponse = await _scheduler.RetryParticipationSyncImmediately(crmData, configuration, true);
            var typedResponse = (CrmResponse)reSyncResponse;

            success = typedResponse.Data.Success;

            if (success)
            {
                var currentParticipation = _participationService.GetParticipation(participation.Id);

                currentParticipation.Status = ParticipationStatus.PARTICIPATION_RETRY_SUCCESS.ToString();
                currentParticipation.ApiStatus = typedResponse.Data.ApiStatus.ToString();
                currentParticipation.ConsumerId = typedResponse.Data.Data.ConsumerId;
                currentParticipation.ApiMessage = typedResponse.Data.ApiMessage;
                currentParticipation.ModifiedDate = DateTimeOffset.UtcNow;
                _participationService.UpdateParticipation(currentParticipation);

                _failedTransactionService.Delete(failed.Id);

                consumerId = typedResponse.Data.Data.ConsumerId;
            }
            else
            {
                var currentParticipation = _participationService.GetParticipation(participation.Id);

                currentParticipation.Status = ParticipationStatus.PARTICIPATION_RETRY_FAILED.ToString();
                currentParticipation.ModifiedDate = DateTimeOffset.UtcNow;
                _participationService.UpdateParticipation(currentParticipation);
            }

            return (success, consumerId);
        }
    }
}
