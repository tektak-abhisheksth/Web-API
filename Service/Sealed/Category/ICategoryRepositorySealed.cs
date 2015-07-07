using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;
using TekTak.iLoop.Category;

namespace TekTak.iLoop.Sealed.Category
{
    public interface ICategoryRepositorySealed : ICategoryRepository
    {
        Task<StatusData<string>> CategoryFriendsCopy(CopyCategory request, SystemSession session);
        Task<PaginatedResponse<IEnumerable<FriendsInCategoryResponse>>> GetFriendsInCategory(PaginatedRequest<FriendsInCategoryRequest> request, SystemSession session);
    }
}
