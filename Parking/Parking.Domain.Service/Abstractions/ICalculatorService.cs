using System;
using System.Threading.Tasks;
using Parking.Domain.Service.DTOs;

namespace Parking.Domain.Service.Abstractions
{
    public interface ICalculatorService
    {
        Task<ParkingRateDto> Calculate(DateTime start, DateTime end);
    }   
}
