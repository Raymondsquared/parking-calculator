using Parking.Domain.Model;
using Parking.Infrastructure.CrossCutting.Abstractions;

namespace Parking.Infrastructure.Abstractions
{
    public interface ISpecialRepository : IRepository<Special>
    {
    }
}
