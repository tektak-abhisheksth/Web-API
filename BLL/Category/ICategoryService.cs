using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Category
{
    public partial interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetUserCategoryList(SystemSession session);
        Task<StatusData<byte?>> InsertCategory(CategoryAddRequest request, SystemSession session);
        Task<StatusData<string>> UpdateCategory(CategoryUpdateRequest request, SystemSession session);
        Task<StatusData<string>> DeleteCategory(DeleteCategory request, SystemDbStatus mode, SystemSession session);
        Task<StatusData<string>> UpsertCategoryFriends(CategoryFriends request, SystemDbStatus mode, SystemSession session);
    }
}
