using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class Address
    {
        [JsonProperty("StreetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("Locality")]
        public string Locality { get; set; }

        [JsonProperty("RecipientName", NullValueHandling = NullValueHandling.Ignore)]
        public string RecipientName { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Standard")]
        public bool Standard { get; set; }

        [JsonProperty("IsPostOfficeBox")]
        public bool IsPostOfficeBox { get; set; }
    }
}
