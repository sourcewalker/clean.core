using Infrastructure.ProCampaign.Ecommerce.Product;
using Infrastructure.ProCampaign.Ecommerce.Order;
using Infrastructure.ProCampaign.Ecommerce.Response;
using Infrastructure.ProCampaign.Ecommerce.Authentication;
using Newtonsoft.Json;

namespace Infrastructure.ProCampaign.Ecommerce.Mapping
{
    public static class Serialize
    {
        public static string ToJson(this EcommerceProductRequest self) => 
            JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this EcommerceOrderRequest self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this EcommerceApiResponse self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);

        public static string ToJson(this EcommerceAuthForm self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
