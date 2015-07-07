using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TekTak.iLoop.Category
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryResponse>> GetUserCategoryList(SystemSession session);
        Task<StatusData<byte?>> InsertCategory(CategoryAddRequest request, SystemSession session);
        Task<StatusData<string>> UpdateCategory(CategoryUpdateRequest request, SystemSession session);
        Task<StatusData<string>> DeleteCategory(DeleteCategory request, SystemSession session);
        Task<StatusData<string>> UpsertCategoryFriends(CategoryFriends request, SystemDbStatus mode, SystemSession session);
    }
}
