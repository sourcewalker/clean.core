using Infrastructure.Community.Interfaces;
using Infrastructure.Community.Models;
using Infrastructure.Community.Status;

namespace Infrastructure.Community.Validation
{
    public class KuhmunityInputValidationModule : IValidateProvider
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string PasswordReconfirm { get; set; }
        public bool TermsAccepted { get; set; }
        public bool PrivacyAccepted { get; set; }

        public KuhmunityResponse Validate()
        {
            KuhmunityResponse response = new KuhmunityResponse
            {
                IsSuccessful = false
            };

            if (string.IsNullOrWhiteSpace(Nickname))
            {
                response.Message = ResponseMessages.MISSING_NICKNAME;

                return response;
            }

            if (!TermsAccepted)
            {
                response.Message = ResponseMessages.KUHMUNITY_TERMS_NOT_ACCEPTED;

                return response;
            }

            if (!PrivacyAccepted)
            {
                response.Message = ResponseMessages.KUHMUNITY_PRIVACY_NOT_ACCEPTED;

                return response;
            }

            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(PasswordReconfirm))
            {
                response.Message = ResponseMessages.MISSING_PASSWORD;

                return response;
            }

            if (!Password.Equals(PasswordReconfirm))
            {
                response.Message = ResponseMessages.PASSWORD_MISMATCH;

                return response;
            }

            response.IsSuccessful = true;
            response.Message = ResponseStatus.OK;

            return response;
        }
    }
}
