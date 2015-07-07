using Model.Common;
using Model.Search;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Search
{
    public interface ISearchService
    {
        Task<PaginatedResponse<IEnumerable<BasicSearchResponse>>> BasicSearch(int userId, string deviceId, string searchTerm, byte? userTypeId, byte? isConnected, int pageIndex, int pageSize);
        Task<UserSearchResponse> UserSearch(UserSearchRequest request, SystemSession session);
    }
}
