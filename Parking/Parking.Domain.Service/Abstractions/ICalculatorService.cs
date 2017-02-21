using System;
using System.Threading.Tasks;

namespace Parking.Domain.Service.Abstractions
{
    public interface ICalculatorService
    {
        Task<double> Calculate(DateTime start, DateTime end);
    }   
}
