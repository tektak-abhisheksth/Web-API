using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Common;
using Model.CountryInfo;

namespace TekTak.iLoop.Data
{
    public interface IDataRepository
    {
        Task<IEnumerable<GeneralKvPair<long, string>>> GetData(string tableName, int? id, string searchString);
        Task<IEnumerable<UserCity>> GetCities(int? id, string searchString);
    }
}
