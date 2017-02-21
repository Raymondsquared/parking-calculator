using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Domain.Model;
using Parking.Domain.Service.Abstractions;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting.Abstractions;

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
    }
}
