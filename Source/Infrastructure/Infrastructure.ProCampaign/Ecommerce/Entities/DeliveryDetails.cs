using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class DeliveryDetails
    {
        [JsonProperty("Address")]
        public Address Address { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
