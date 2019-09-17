using Newtonsoft.Json;

namespace Web.Service.REST.Models
{
    [JsonObject]
    public class ExtractModel
    {
        [JsonProperty(PropertyName = "Email hash", Order = 0)]
        public string EmailHash { get; set; }

        [JsonProperty(PropertyName = "Consumer Id", Order = 1)]
        public string ConsumerId { get; set; }

        [JsonProperty(PropertyName = "Participation date", Order = 5)]
        public string ParticipationDate { get; set; }
    }
}
