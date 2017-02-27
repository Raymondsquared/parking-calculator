using System;
using Parking.Infrastructure.CrossCutting.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Infrastructure.CrossCutting.Validators
{
    public class InputDateValidator : IValidator<string>
    {
        public ValidationDto IsValid(string input)
        {
            var result = new ValidationDto() { IsValid = true };

            try
            {
                if (Convert.ToDateTime(input) == default(DateTime))
                {
                    result.IsValid = false;
                    result.ErrorMessage = "Invalid date";
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
