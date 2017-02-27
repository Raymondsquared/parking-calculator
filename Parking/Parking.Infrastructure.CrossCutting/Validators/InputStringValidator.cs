using Parking.Infrastructure.CrossCutting.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Infrastructure.CrossCutting.Validators
{
    public class InputStringValidator : IValidator<string>
    {
        public ValidationDto IsValid(string input)
        {
            var result = new ValidationDto() { IsValid = true };

            if (string.IsNullOrEmpty(input))
            {
                result.IsValid = false;
                result.ErrorMessage = "Input cannot be empty";
            }

            return result;
        }
    }
}
