using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Category
{

    public partial class CategoryService
    {
        public Task<StatusData<string>> CategoryFriendsCopy(CopyCategory request, SystemSession session)
        {
            return _jUnitOfWork.Category.CategoryFriendsCopy(request, session);
        }

        public Task<PaginatedResponse<IEnumerable<FriendsInCategoryResponse>>> GetFriendsInCategory(PaginatedRequest<FriendsInCategoryRequest> request, SystemSession session)
        {
            return _jUnitOfWork.Category.GetFriendsInCategory(request, session);
        }
    }
}
