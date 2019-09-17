using System;
using Infrastructure.ProCampaign.Ecommerce.Entities;
using Infrastructure.ProCampaign.Ecommerce.Mapping;
using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Product
{
    public class EcommerceProductRequest
    {
        [JsonProperty("Availability")]
        public Availability Availability { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("Classifications")]
        public Classification[] Classifications { get; set; }

        [JsonProperty("ContentLanguage")]
        public string ContentLanguage { get; set; }

        [JsonProperty("CustomAttributes")]
        public CustomAttribute[] CustomAttributes { get; set; }

        [JsonProperty("DeliveryInfo")]
        public DeliveryInfoType DeliveryInfo { get; set; }

        [JsonProperty("Links")]
        public ProductLink[] Links { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ExternalId")]
        public string ExternalId { get; set; }

        [JsonProperty("GTIN")]
        public string Gtin { get; set; }

        [JsonProperty("MPN")]
        public string Mpn { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ImageLink")]
        public Uri ImageLink { get; set; }

        [JsonProperty("MultiPack")]
        public long MultiPack { get; set; }

        [JsonProperty("Price")]
        public Price Price { get; set; }

        [JsonProperty("Shop")]
        public Entities.Shop Shop { get; set; }

        [JsonProperty("StockLevel")]
        public long StockLevel { get; set; }

        [JsonProperty("TargetCountry")]
        public string TargetCountry { get; set; }

        [JsonProperty("TaxRate")]
        public decimal TaxRate { get; set; }

        public static EcommerceProductRequest FromJson(string json) => 
            JsonConvert.DeserializeObject<EcommerceProductRequest>(json, Converter.Settings);
    }
}
