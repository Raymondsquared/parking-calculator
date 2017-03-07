using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Infrastructure.CrossCutting.DTOs;
using Parking.Infrastructure.CrossCutting.Validators;

namespace Parking.Application.Abstractions
{
    public interface IApplicationService
    {
        Validation ValidateConsoleInput(IList<string> input, out TimerDto timer);
        Task<string> ProcessAsync(TimerDto input);
    }
}
