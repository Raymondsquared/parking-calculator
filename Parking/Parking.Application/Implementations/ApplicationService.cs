using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parking.Application.Abstractions;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.CrossCutting.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;
using Parking.Infrastructure.CrossCutting.Validators;

namespace Parking.Application.Implementations
{
    public class ApplicationService : IApplicationService
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IEnumerable<IValidator<string>> _consoleInputValidators;
        private readonly IEnumerable<IValidator<TimerDto>> _datesValidators;

        public ApplicationService(
            ICalculatorService calculatorService,
            IEnumerable<IValidator<string>> consoleInputValidators,
            IEnumerable<IValidator<TimerDto>> datesValidators
            )
        {
            _calculatorService = calculatorService;
            _consoleInputValidators = consoleInputValidators;
            _datesValidators = datesValidators;
        }
        
        public async Task<string> ProcessAsync(TimerDto input)
        {
            var response = await _calculatorService.Calculate(input);
            var result = 
                "\nPackage : " + response.Name + 
                "\nTOTAL COST : " + response.Price.ToString("C");

            return result;
        }
        
        // TODO: orchestrate with Chain of Responsibility pattern
        public Validation ValidateConsoleInput(IList<string> input, out TimerDto timer)
        {
            timer = new TimerDto();

            foreach (var validator in _consoleInputValidators)
            {
                foreach (var i in input)
                {
                    var r = validator.IsValid(i);
                    if (!r.IsValid)
                    {
                        return r;
                    }
                }
            }

            // Add them into the out objects
            timer.Entry = Convert.ToDateTime(input.ElementAt(0));
            timer.Exit = Convert.ToDateTime(input.ElementAt(1));
            
            foreach (var validator in _datesValidators)
            {
                var r = validator.IsValid(timer);
                if (!r.IsValid)
                {
                    return r;
                }
            }
           
            return new Validation() {IsValid = true};
        }
    }
}
