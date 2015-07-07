
using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Category
{
    public partial interface ICategoryService
    {
        Task<StatusData<string>> CategoryFriendsCopy(CopyCategory request, SystemSession session);
        Task<PaginatedResponse<IEnumerable<FriendsInCategoryResponse>>> GetFriendsInCategory(PaginatedRequest<FriendsInCategoryRequest> request, SystemSession session);
    }
}
