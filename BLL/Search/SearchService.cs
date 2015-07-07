using DAL.DbEntity;
using Model.Common;
using Model.Search;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Search
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public SearchService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<PaginatedResponse<IEnumerable<BasicSearchResponse>>> BasicSearch(int userId, string deviceId, string searchTerm, byte? userTypeId, byte? isConnected, int pageIndex, int pageSize)
        {
            return _unitOfWork.Search.BasicSearch(userId, deviceId, searchTerm, userTypeId, isConnected, pageIndex, pageSize);
        }

        public Task<UserSearchResponse> UserSearch(UserSearchRequest request, SystemSession session)
        {
            return _jUnitOfWork.Search.UserSearch(request, session);
        }
    }
}
