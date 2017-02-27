using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Infrastructure.CrossCutting.DTOs;

namespace Parking.Application.Abstractions
{
    public interface IApplicationService
    {
        ValidationDto ValidateConsoleInput(IList<string> input, out TimerDto timer);
        Task<string> ProcessAsync(TimerDto input);
    }
}
