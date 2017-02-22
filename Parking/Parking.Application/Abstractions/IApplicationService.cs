using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Application.Abstractions
{
    public interface IApplicationService
    {
        bool ValidateInput(IEnumerable<string> input, out string message, out List<DateTime> dts);
        Task<string> ProcessAsync(IEnumerable<DateTime> input);
    }
}
