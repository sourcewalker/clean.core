using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class CustomAttribute
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public long Type { get; set; }

        [JsonProperty("Value")]
        public double Value { get; set; }
    }
}
