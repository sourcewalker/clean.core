using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class Consumer
    {
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Addresses")]
        public Address[] Addresses { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
