using Newtonsoft.Json;
using System;
using Infrastructure.ProCampaign.Ecommerce.Mapping;

namespace Infrastructure.ProCampaign.Ecommerce.Response
{
    public class EcommerceApiResponse
    {
        [JsonProperty("Data")]
        public object Data { get; set; }

        [JsonProperty("JobId")]
        public Guid JobId { get; set; }

        [JsonProperty("HttpStatusCode")]
        public long HttpStatusCode { get; set; }

        [JsonProperty("HttpStatusMessage")]
        public string HttpStatusMessage { get; set; }

        [JsonProperty("StatusCode")]
        public long StatusCode { get; set; }

        [JsonProperty("StatusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("IsSuccessful")]
        public bool IsSuccessful { get; set; }

        public static EcommerceApiResponse FromJson(string json) =>
            JsonConvert.DeserializeObject<EcommerceApiResponse>(json, Converter.Settings);
    }
}
