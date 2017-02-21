using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Domain.Model;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting.Helper;

namespace Parking.Infrastructure.Repositories
{
    public class SpecialJsonRepository : ISpecialRepository
    {
        public async Task<IEnumerable<Special>> SelectAllAsync()
        {
            // Default location
            // 'D:\\development\\parking-calculator\\Parking\\Parking.Console\\bin\\Debug\\special.json'
            var path = "special.json";
            var data = FileHelper.Read(FileHelper.PersistancePath + path);
            var result = JsonHelper.Deserialise<IEnumerable<Special>>(data);

            return result;
        }
    }
}
