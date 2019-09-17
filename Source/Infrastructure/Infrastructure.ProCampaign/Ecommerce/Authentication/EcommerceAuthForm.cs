using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Authentication
{
    public class EcommerceAuthForm
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
