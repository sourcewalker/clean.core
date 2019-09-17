using Core.Infrastructure.Interfaces.CRM;
using Newtonsoft.Json;
using System;

namespace Infrastructure.ProCampaign.Models
{
    public class ProCampaignData
    {
        public static string FormatParticipationData(CrmData data)
        {
            var participation = new
            {
                Source = data.GetSetting<string>("SourceName"),
                Attributes = new[]
                {
                    new
                    {
                        Name = "Email",
                        Value = data.Data.Email
                    },
                    new
                    {
                        Name = "Country",
                        Value = data.Data.Country
                    },
                    new
                    {
                        Name = "list:Privacy_Policy_EN",
                        Value = (object)1
                    },
                    new
                    {
                        Name = "List:CBZZ190301_Participants",
                        Value = (object)1
                    },
                    new
                    {
                        Name = "list:Cadbury_Email",
                        Value = data.Data.NewsletterOptin ? (object)1 : (object)string.Empty
                    }
                },
                Transactions = new[]
                {
                    new
                    {
                        Name = data.GetSetting<string>("TransactionName"),
                        Date_Created = DateTimeOffset.UtcNow.ToString(),
                        Parameters = new []
                        {
                            new
                            {
                                Name = "Ident_long",
                                Value = data.Data.Retailer
                            },
                            new
                            {
                                Name = "Ident_short",
                                Value = data.Data.ChocolateBar
                            },
                            new
                            {
                                Name = "Q1",
                                Value = data.Data.Place
                            },
                            new
                            {
                                Name = "Q2",
                                Value = data.Data.Q2
                            }
                        }
                    }
                },
                LegalTextVersions = new[]
                {
                    new
                    {
                        LegalTextName = data.Data.PrivacyPolicyTextName,
                        Version = data.Data.PrivacyPolicyVersion,
                        Created = data.Data.PrivacyPolicyCreation
                    }
                }
            };

            return JsonConvert.SerializeObject(participation);
        }

        public static string FormatMopupData(CrmData data)
        {
            var participation = new
            {
                Source = data.GetSetting<string>("SourceName"),
                Attributes = new[]
                {
                    new
                    {
                        Name = "Email",
                        Value = data.Data.Email
                    },
                    new
                    {
                        Name = "Country",
                        Value = data.Data.Country
                    },
                    new
                    {
                        Name = "list:Privacy_Policy_EN",
                        Value = (object)1
                    },
                    new
                    {
                        Name = "List:CBZZ190301_Participants",
                        Value = (object)1
                    },
                    new
                    {
                        Name = "list:Cadbury_Email",
                        Value = data.Data.NewsletterOptin ? (object)1 : (object)string.Empty
                    }
                },
                Transactions = new[]
                {
                    new
                    {
                        Name = data.GetSetting<string>("TransactionName"),
                        Date_Created = DateTimeOffset.UtcNow.ToString()
                    }
                },
                LegalTextVersions = new[]
                {
                    new
                    {
                        LegalTextName = data.Data.PrivacyPolicyTextName,
                        Version = data.Data.PrivacyPolicyVersion,
                        Created = data.Data.PrivacyPolicyCreation
                    }
                }
            };

            return JsonConvert.SerializeObject(participation);
        }
    }
}
