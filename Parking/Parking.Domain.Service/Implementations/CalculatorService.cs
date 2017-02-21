using System;
using System.Threading.Tasks;
using Parking.Domain.Model;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.CrossCutting.Abstractions;

namespace Parking.Domain.Service.Implementations
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IDomainService<Special> _specialService;
        private readonly IDomainService<Normal> _normalService;

        public CalculatorService(ISpecialService specialService, INormalService normalService)
        {
            _specialService = specialService;
            _normalService = normalService;
        }

        public async Task<double>Calculate(DateTime start, DateTime end)
        {
            var special = await _specialService.GetAllAsync();
            var normal = await _normalService.GetAllAsync();
            return 0;
        }
    }
}
