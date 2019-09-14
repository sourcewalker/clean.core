namespace Core.Service.Interfaces
{
    public interface IValidationService
    {
        bool ValidateCaptcha(string response);

        bool HasAlreadyParticipated(string email);
    }
}
