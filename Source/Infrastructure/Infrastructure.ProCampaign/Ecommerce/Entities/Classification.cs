using System;
using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Entities
{
    public class Classification
    {
        [JsonProperty("Accessibility")]
        public ClassificationAccessibilityType Accessibility { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
