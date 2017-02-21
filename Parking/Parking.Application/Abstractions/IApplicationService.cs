using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Application.Abstractions
{
    public interface IApplicationService
    {
        Task<string> ProcessAsync(IEnumerable<string> input);
    }
}
