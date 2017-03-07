using Parking.Infrastructure.CrossCutting.Validators;

namespace Parking.Infrastructure.CrossCutting.Abstractions
{
    public interface IValidator<in T>
    {
        Validation IsValid(T input);
    }
}