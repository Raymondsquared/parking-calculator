using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<double> Calculate(DateTime start, DateTime end)
        {
            var specialRates = await _specialService.GetAllAsync();
            var normalRates = await _normalService.GetAllAsync();

            var result = 0.0;

            // TODO: Refactor this with Abstraction like IValidator 
            // TODO: and orchestrate with Chain of Responsibility pattern
            var resultSpecial = calculateSpecial(specialRates, start, end);
            result = resultSpecial;
            
            var resultNormal = calculateNormal(normalRates, start, end);
           
            // choose for either normal vs special
            if (resultNormal > 0 && (result == 0 || result > resultNormal))
            {
                result = resultNormal;
            }

            return result;
        }

        // TODO: Refactor this with Abstraction like IValidator 
        // TODO: and orchestrate with Chain of Responsibility pattern
        private double calculateNormal(IEnumerable<Normal> normalRates, DateTime start, DateTime end)
        {
            var result = 0.0;

            var resultNormalMacro = 0.0;
            var resultNormalMicro = 0.0;
            var isNormal = false;
            var duration = (end - start).TotalHours;
            var maxNormalRate = normalRates.OrderBy(nr => nr.MaxHours).LastOrDefault();

            // More than max duration
            if (duration >= maxNormalRate.MaxHours)
            {
                resultNormalMacro = Math.Floor(duration / maxNormalRate.MaxHours) * maxNormalRate.Rate;
                duration = duration % maxNormalRate.MaxHours;
            }
            if (duration > 0)
            {
                foreach (var normalRate in normalRates)
                {
                    if (!isNormal && duration <= normalRate.MaxHours)
                    {
                        isNormal = true;
                        resultNormalMicro = normalRate.Rate;
                    }
                }
            }

            result = resultNormalMacro + resultNormalMicro;
            
            return result;
        }

        // TODO: Refactor this with Abstraction like IValidator 
        // TODO: and orchestrate with Chain of Responsibility pattern
        private double calculateSpecial(IEnumerable<Special> specialRates, DateTime start, DateTime end)
        {
            var result = 0.0;

            foreach (var specialRate in specialRates)
            {
                var counter = 0;

                // Entry
                bool isSpecial = (specialRate.Entry.Start <= start.TimeOfDay && start.TimeOfDay <= specialRate.Entry.End) ||
                                 (specialRate.MaxDays > 0 &&
                                  (specialRate.Entry.Start <= start.TimeOfDay &&
                                   start.TimeOfDay <= specialRate.Entry.End.Add(TimeSpan.FromDays(1))) ||
                                  (specialRate.Entry.Start.Subtract(TimeSpan.FromDays(1)) <= start.TimeOfDay &&
                                   start.TimeOfDay <= specialRate.Entry.End));


                if (
                    !specialRate.Entry.Days.Any(
                        d => string.Equals(d, start.DayOfWeek.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                {
                    isSpecial = false;
                }

                // Exit
                var maxExitDay = start.AddDays(specialRate.MaxDays);
                var maxExit = new DateTime(maxExitDay.Year, maxExitDay.Month, maxExitDay.Day, specialRate.Exit.End.Hours,
                    specialRate.Exit.End.Minutes, 0);
                if (end > maxExit)
                {
                    isSpecial = false;
                }

                if (!specialRate.Exit.Days.Any(
                        d => string.Equals(d, end.DayOfWeek.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                {
                    isSpecial = false;
                }

                // Max days
                if ((end - start).Days > specialRate.MaxDays)
                {
                    isSpecial = false;
                }

                if (isSpecial)
                {
                    if (result == 0 || result > specialRate.TotalPrice)
                    {
                        result = specialRate.TotalPrice;
                    }
                }
            }

            return result;

        }
    }
}
