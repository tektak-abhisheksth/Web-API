using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Category;
using Model.Types;

namespace DAL.Category
{
    public interface ICategoryRepository : IGenericRepository<UserCategoryFriend>
    {
        Task<IEnumerable<CategoryResponse>> GetUserCategoryList(int userId);
        Task<SystemDbStatus> UpsertCategory(CategoryFriends request, int categoryId, SystemDbStatus mode);
        //SystemDbStatus CopyCategory(CopyCategory request, SystemDatabaseOperationMode operation);
        //Task<PaginatedResponse<IEnumerable<int>>> GetFriendsInCategoryList(int userId, int userCategoryTypeId, int? pageIndex, int? pageSize);
        Task<SystemDbStatus> DeleteCategory(DeleteCategory request, SystemDbStatus mode);
    }
}
