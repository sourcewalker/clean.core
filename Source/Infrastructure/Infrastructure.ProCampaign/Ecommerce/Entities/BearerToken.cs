using System;
using Infrastructure.ProCampaign.Ecommerce.Entities;
using Infrastructure.ProCampaign.Ecommerce.Mapping;
using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class BearerToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiryStamp { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty(".issued")]
        public DateTime IssuedDate { get; set; }

        [JsonProperty(".expires")]
        public DateTime ExpirationDate { get; set; }

        public static BearerToken FromJson(string json) =>
            JsonConvert.DeserializeObject<BearerToken>(json, Converter.Settings);
    }
}
