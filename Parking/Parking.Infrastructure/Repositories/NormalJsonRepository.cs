using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Domain.Model;
using Parking.Infrastructure.Abstractions;
using Parking.Infrastructure.CrossCutting.Helpers;

namespace Parking.Infrastructure.Repositories
{
    public class NormalJsonRepository : INormalRepository
    {
        public async Task<IEnumerable<Normal>> SelectAllAsync()
        {
            // Default location
            // 'D:\\development\\parking-calculator\\Parking\\Parking.Console\\bin\\Debug\\normal.json'
            var path = "normal.json";
            var data = FileHelper.Read(FileHelper.PersistancePath + path);
            var result = JsonHelper.Deserialise<IEnumerable<Normal>>(data);

            return result;
        }
    }
}
