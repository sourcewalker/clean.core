using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class OrderStatus
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
