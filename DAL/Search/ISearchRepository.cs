using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Common;
using Model.Search;

namespace DAL.Search
{
    public interface ISearchRepository : IGenericRepository<UserLogin>
    {
        Task<PaginatedResponse<IEnumerable<BasicSearchResponse>>> BasicSearch(int userId, string deviceId, string searchTerm, byte? userTypeId, byte? isConnected, int pageIndex, int pageSize);
    }
}
