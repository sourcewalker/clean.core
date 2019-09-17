using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class Price
    {
        [JsonProperty("Value")]
        public decimal Value { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }
    }
}
