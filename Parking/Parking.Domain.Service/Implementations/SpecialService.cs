using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parking.Domain.Model;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Domain.Service.Implementations
{
    public class SpecialService : ISpecialService
    {
        private readonly IRepository<Special> _specialRepository;

        public SpecialService(ISpecialRepository specialRepository)
        {
            _specialRepository = specialRepository;
        }

        public async Task<IEnumerable<Special>> GetAllAsync()
        {
            return await _specialRepository.SelectAllAsync();
        }
       
        // TODO: Refactor this with Abstraction like IValidator 
        // TODO: and orchestrate with Chain of Responsibility pattern
        public ParkingRateDto Calculate(IEnumerable<Special> specialRates, DateTime start, DateTime end)
        {
            var result = new ParkingRateDto();

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
                    if (result.Price == 0 || result.Price > specialRate.TotalPrice)
                    {
                        result.Name = specialRate.Name;
                        result.Price = specialRate.TotalPrice;
                    }
                }
            }

            return result;

        }
    }
}
