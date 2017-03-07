using System.Threading.Tasks;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Domain.Service.Implementations
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ISpecialService _specialService;
        private readonly INormalService _normalService;

        public CalculatorService(ISpecialService specialService, INormalService normalService)
        {
            _specialService = specialService;
            _normalService = normalService;
        }

        public async Task<ParkingRateDto> Calculate(TimerDto input)
        {
            var specialRates = await _specialService.GetAllAsync();
            var normalRates = await _normalService.GetAllAsync();

            var result = new ParkingRateDto();

            // TODO: Refactor this with Abstraction like IValidator 
            // TODO: and orchestrate with Chain of Responsibility pattern
            var resultSpecial = _specialService.Calculate(specialRates, input.Entry, input.Exit);
            result = resultSpecial;
            
            var resultNormal = _normalService.Calculate(normalRates, input.Entry, input.Exit);
           
            // choose for either normal vs special
            if (resultNormal.Price > 0 && (result.Price == 0 || result.Price > resultNormal.Price))
            {
                result.Name = resultNormal.Name;
                result.Price = resultNormal.Price;
            }

            return result;
        }
    }
}
