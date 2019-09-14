using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net;

namespace Infrastructure.Captcha.Helper
{
    public static class GoogleRecaptchaHelper
    {

        public static bool ValidateReCaptchaV2(IConfiguration configuration, string captchaResponse)
        {
            if (!Convert.ToBoolean(configuration["Captcha:ServerValidationEnabled"]))
            {
                return true;
            }

            var apiRequest = configuration["Captcha:ApiRequest"]
                                                 .ToString(CultureInfo.InvariantCulture);
            var secretKey = configuration["Captcha:SecretKey"]
                                                .ToString(CultureInfo.InvariantCulture);
            apiRequest = string.Format(apiRequest, secretKey, captchaResponse);

            var response = false;

            using (var webClient = new WebClient())
            {
                var jsonStr = webClient.DownloadString(apiRequest);

                JToken token = JObject.Parse(jsonStr);
                var success = token.SelectToken("success").ToString().ToLower();

                if (!string.IsNullOrEmpty(success) && success != "false")
                    response = true;
            }

            return response;
        }
    }
}
