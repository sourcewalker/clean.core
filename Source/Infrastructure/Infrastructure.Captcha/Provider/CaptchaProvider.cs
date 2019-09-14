using Core.Infrastructure.Interfaces.Validator;
using Infrastructure.Captcha.Helper;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Captcha.Provider
{
    public class CaptchaProvider : IFormValidatorProvider
    {
        private readonly IConfiguration _config;

        public CaptchaProvider(IConfiguration config)
        {
            _config = config;
        }

        public bool Validate(string response)
        {
            return GoogleRecaptchaHelper.ValidateReCaptchaV2(_config, response);
        }
    }
}
