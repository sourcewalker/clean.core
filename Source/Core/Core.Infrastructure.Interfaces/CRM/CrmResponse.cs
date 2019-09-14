namespace Core.Infrastructure.Interfaces.CRM
{
    public class CrmResponse
    {
        public Response Data { get; set; }
    }

    public class Response
    {

        public bool Success { get; set; }

        public long ApiStatus { get; set; }

        public string ApiMessage { get; set; }

        public long HttpStatus { get; set; }

        public string HttpMessage { get; set; }

        public Data Data { get; set; }
    }

    public class Data
    {
        public string ConsumerId { get; set; }
    }
}
