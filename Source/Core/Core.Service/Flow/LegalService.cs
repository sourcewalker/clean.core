using Core.Infrastructure.Interfaces.Configuration;
using Core.Infrastructure.Interfaces.CRM;
using Core.Infrastructure.Interfaces.Logging;
using Core.Service.Interfaces;
using Core.Shared.Models;
using System.Threading.Tasks;

namespace Core.Service.Flow
{
    public class LegalService : ILegalService
    {
        private readonly IParticipationService _votingService;
        private readonly ILoggingProvider _logger;
        private readonly ICrmConsumerProvider _crmProvider;
        private readonly IConfigurationProvider _configProvider;

        public LegalService
        (
            IParticipationService votingService,
            ILoggingProvider logger,
            ICrmConsumerProvider crmProvider,
            IConfigurationProvider configProvider
        )
        {
            _votingService = votingService;
            _logger = logger;
            _crmProvider = crmProvider;
            _configProvider = configProvider;
        }

        public async Task<string> GetPrivacyPolicyTextAsync()
        {
            _crmProvider.Configuration.AddSetting("Environment", Environments.Local);
            _crmProvider.Configuration.AddSetting("ApiBaseUrl",
                _configProvider.GetSharedConfig("CSX:Api:BaseUrl"));
            _crmProvider.Configuration.AddSetting("PrivacyPath",
                $"{_configProvider.GetSharedConfig("CSX:Api:DocumentPath")}/" +
                $"{_configProvider.GetSharedConfig("CSX:Api:ListName")}/" +
                $"{_configProvider.GetSharedConfig("CSX:Api:DocumentName")}");
            _crmProvider.Configuration.AddSetting("InternationalApiKey",
                _configProvider.GetSharedConfig("CSX:Api:ApiKey"));

            var privacy = await _crmProvider.ReadTextDocumentAsync();

            return privacy.Data.Data.Html;
        }
    }
}
