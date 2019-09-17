using Core.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Web.Service.REST.Validation
{
    public class Captcha : ValidationAttribute
    {

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var validationService = (IValidationService)validationContext.GetService(typeof(IValidationService));

            return validationService.ValidateCaptcha((string)value) ?
                        ValidationResult.Success :
                        new ValidationResult(ErrorMessage);

        }
    }
}
