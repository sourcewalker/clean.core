using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class Payment
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}
