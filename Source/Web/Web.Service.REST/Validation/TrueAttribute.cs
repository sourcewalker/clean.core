using System.ComponentModel.DataAnnotations;

namespace Web.Service.REST.Validation
{
    public class True : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage);

            return (bool)value ?
                ValidationResult.Success :
                new ValidationResult(ErrorMessage);
        }
    }
}
