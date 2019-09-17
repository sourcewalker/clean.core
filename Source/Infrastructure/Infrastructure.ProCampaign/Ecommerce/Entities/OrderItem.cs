using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class OrderItem
    {
        [JsonProperty("Position")]
        public int Position { get; set; }

        [JsonProperty("Product")]
        public Product Product { get; set; }

        [JsonProperty("QuantityOrdered")]
        public long QuantityOrdered { get; set; }

        [JsonProperty("Price")]
        public Price Price { get; set; }

        [JsonProperty("Tax")]
        public Price Tax { get; set; }
    }
}
