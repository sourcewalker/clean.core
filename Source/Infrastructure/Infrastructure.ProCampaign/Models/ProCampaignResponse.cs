using System.Dynamic;

namespace Infrastructure.ProCampaign.Models
{
    public class ProCampaignResponse
    {
        public ExpandoObject Data { get; set; }

        public string JobId { get; set; }

        public int HttpStatusCode { get; set; }

        public string HttpStatusMessage { get; set; }

        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public bool IsSuccessful { get; set; }

    }
}
