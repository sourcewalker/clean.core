using Newtonsoft.Json;
using System;

namespace Infrastructure.ProCampaign.Ecommerce.Authentication
{
    public class EcommerceAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpirationStamp { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("roles")]
        public string Roles { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty(".issued")]
        public DateTime IssuedDate { get; set; }

        [JsonProperty(".expires")]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
