using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Infrastructure.CrossCutting.Abstractions
{
    public interface IValidator<in T>
    {
        ValidationDto IsValid(T input);
    }
}