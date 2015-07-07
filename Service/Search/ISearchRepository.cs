using Model.Search;
using Model.Types;
using System.Threading.Tasks;

namespace TekTak.iLoop.Search
{
    public interface ISearchRepository
    {
        Task<UserSearchResponse> UserSearch(UserSearchRequest request, SystemSession session);
    }
}
