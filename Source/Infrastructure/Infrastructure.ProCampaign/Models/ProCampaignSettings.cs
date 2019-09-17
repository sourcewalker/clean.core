using System;

namespace Infrastructure.ProCampaign.Models
{
    public class ProCampaignSettings
    {
        public Uri ConsumerBaseUrl { get; internal set; }

        public Uri ApiBaseUrl { get; internal set; }

        public string ParticipationPath { get; internal set; }

        public string DocumentPath { get; set; }

        public string ApiKey { get; internal set; }

        public string ApiSecret { get; internal set; }

        public string InternationalApiKey { get; set; }
    }
}
