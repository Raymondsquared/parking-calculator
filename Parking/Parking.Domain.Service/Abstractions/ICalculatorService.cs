using System.Threading.Tasks;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Domain.Service.Abstractions
{
    public interface ICalculatorService
    {
        Task<ParkingRateDto> Calculate(TimerDto input);
    }   
}
