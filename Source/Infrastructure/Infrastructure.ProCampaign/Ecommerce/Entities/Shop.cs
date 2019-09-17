using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class Shop
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
    }
}
