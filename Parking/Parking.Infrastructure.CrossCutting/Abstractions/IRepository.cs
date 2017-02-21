using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Infrastructure.CrossCutting.Abstractions
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> SelectAllAsync();
    }
}
