using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Application.Abstractions;
using Parking.Domain.Service.Abstractions;

namespace Parking.Application.Implementations
{
    public class ApplicationService : IApplicationService
    {
        private readonly ICalculatorService _calculatorService;

        public ApplicationService(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public async Task<string> ProcessAsync(IEnumerable<string> input)
        {
            var response = await _calculatorService.Calculate(new DateTime(), new DateTime());
            return response.ToString();
        }
    }
}
