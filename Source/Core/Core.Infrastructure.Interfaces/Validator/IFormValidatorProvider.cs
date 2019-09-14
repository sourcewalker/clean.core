namespace Core.Infrastructure.Interfaces.Validator
{
    public interface IFormValidatorProvider
    {
        bool Validate(string response);
    }
}
