using System;
using System.Collections.Generic;
using System.Linq;
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
        

        public async Task<string> ProcessAsync(IEnumerable<DateTime> input)
        {
            var start = Convert.ToDateTime(input.ElementAt(0));
            var end = Convert.ToDateTime(input.ElementAt(1));

            var response = await _calculatorService.Calculate(start, end);
            var result = string.Empty;
            result += 
                "\nPackage : " + response.Name + 
                "\nTOTAL COST : " + response.Price.ToString("C");

            return result;
        }
        
        // TODO: Refactor this with Abstraction like IValidator 
        // TODO: and orchestrate with Chain of Responsibility pattern
        public bool ValidateInput(IEnumerable<string> input, out string message, out List<DateTime> dts)
        {
            var result = true;

            message = string.Empty;
            dts = new List<DateTime>();

            try
            {
                var start = Convert.ToDateTime(input.ElementAt(0));
                var end = Convert.ToDateTime(input.ElementAt(1));

                if (end <= start)
                {
                    message = "End date is greater than start date\n";
                    result = false;
                }

                dts.Add(start);
                dts.Add(end);
            }
            catch (Exception ex)
            {
                message = ex.Message + "\n";
                result = false;
            }

            return result;
        }
    }
}
