using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parking.Domain.Model;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting;
using Parking.Infrastructure.CrossCutting.Abstractions;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Domain.Service.Implementations
{
    public class NormalService : INormalService
    {
        private readonly IRepository<Normal> _normalRepository;

        public NormalService(INormalRepository normalRepository)
        {
            _normalRepository = normalRepository;
        }

        public async Task<IEnumerable<Normal>> GetAllAsync()
        {
            return await _normalRepository.SelectAllAsync();
        }

        // TODO: Refactor this with Abstraction like IValidator 
        // TODO: and orchestrate with Chain of Responsibility pattern
        public ParkingRateDto Calculate(IEnumerable<Normal> normalRates, DateTime start, DateTime end)
        {
            var result = new ParkingRateDto() { Name = CONSTANTS.RATE.STANDARD };

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

            result.Price = resultNormalMacro + resultNormalMicro;

            return result;
        }

    }
}
