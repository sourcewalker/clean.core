namespace Infrastructure.ProCampaign.Constant
{
    internal struct ProCampaignConstants
    {
        internal struct ConsumerAPI
        {
            internal const string BaseUrl = "https://consumer.procampaignapi.com";

            internal const string ParticipationPath = "Consumer";
        }

        internal struct DocumentAPI
        {
            internal const string BaseUrl = "https://api.procampaignapi.com";

            internal const string DefaultListName = "Privacy_Policy_EN";

            internal const string DefaultDocumentName = "PrivacyPolicy_EN";

            internal const string DocumentPath = "Consumer/Documents";

            internal const string InternationalApiKey = "S1JBRlQuSU5UTCBWN3xNT1paMTgwMTAy";

        }
    }
}
