using System;
using Infrastructure.ProCampaign.Ecommerce.Entities;
using Infrastructure.ProCampaign.Ecommerce.Mapping;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Infrastructure.ProCampaign.Ecommerce.Order
{
    public class EcommerceOrderRequest
    {
        [JsonProperty("Consumer")]
        public Entities.Consumer Consumer { get; set; }

        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("DeliveryDetails")]
        public DeliveryDetails DeliveryDetails { get; set; }

        [JsonProperty("Items")]
        public List<OrderItem> Items { get; set; }

        [JsonProperty("NetAmount")]
        public Price NetAmount { get; set; }

        [JsonProperty("OrderId")]
        public string OrderId { get; set; }

        [JsonProperty("PaymentInfo")]
        public Payment PaymentInfo { get; set; }

        [JsonProperty("PaymentStatus")]
        public PaymentStatus PaymentStatus { get; set; }

        [JsonProperty("ShippingCosts")]
        public Price ShippingCosts { get; set; }

        //[JsonProperty("ShippingTax")]
        //public Price ShippingTax { get; set; }

        [JsonProperty("Shop")]
        public Entities.Shop Shop { get; set; }

        [JsonProperty("Status")]
        public OrderStatus Status { get; set; }

        [JsonProperty("TotalAmount")]
        public Price TotalAmount { get; set; }

        public static EcommerceOrderRequest FromJson(string json) =>
            JsonConvert.DeserializeObject<EcommerceOrderRequest>(json, Converter.Settings);
    }
}
