using Core.Infrastructure.Interfaces.DAL;
using Core.Infrastructure.Interfaces.Logging;
using Core.Infrastructure.Interfaces.Validator;
using Core.Service.Interfaces;
using Core.Shared.Utility;
using System;

namespace Core.Service.Flow
{
    public class ValidationService : IValidationService
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly ILoggingProvider _logger;
        private readonly IFormValidatorProvider _validator;

        public ValidationService(
            ILoggingProvider logger,
            IParticipationRepository participationRepository,
            IFormValidatorProvider validator)
        {
            _logger = logger;
            _participationRepository = participationRepository;
            _validator = validator;
        }

        public bool ValidateCaptcha(string captchaResponse)
        {
            if (captchaResponse == null)
                return false;

            var response = false;
            try
            {
                return _validator.Validate(captchaResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Captcha validation failed", ex);
            }
            return response;
        }

        public bool HasAlreadyParticipated(string email)
        {
            var emailHash = StringUtility.Md5HashEncode(email.ToLower());
            return _participationRepository.GetByEmailHash(emailHash) != null;
        }
    }
}
