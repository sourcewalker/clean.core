using Core.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Web.Service.REST.Validation
{
    public class NotUsedEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var validationService = (IValidationService)validationContext.GetService(typeof(IValidationService));

            return !validationService.HasAlreadyParticipated((string)value) ?
                        ValidationResult.Success :
                        new ValidationResult(ErrorMessage);

        }
    }
}
