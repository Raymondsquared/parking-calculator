using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Domain.Model;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting.Abstractions;

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
    }
}
