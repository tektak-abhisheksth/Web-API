using DAL.DbEntity;
using Entity;
using Model.Category;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Category
{
    public class CategoryRepository : GenericRepository<UserCategoryFriend>, ICategoryRepository
    {
        public CategoryRepository(iLoopEntity context) : base(context) { }

        public async Task<IEnumerable<CategoryResponse>> GetUserCategoryList(int userId)
        {
            var result = await Task.Factory.StartNew(() => Context.PROC_CATEGORY_LIST(userId)).ConfigureAwait(false);
            return result.Select(c => new CategoryResponse
            {
                Name = c.NAME,
                CategoryId = c.USERCATEGORYTYPEID,
                Description = c.DESCRIPTION,
                Friends = c.Friends == null ? new List<int>() : c.Friends.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)),
                IsSystemDefaultCategory = c.ISSYSTEMDEFAULT == 1
            });
        }

        public async Task<SystemDbStatus> DeleteCategory(DeleteCategory request, SystemDbStatus mode)
        {
            var categoryIdsObj = new ObjectParameter("USERCATEGORYTYPEIDS", string.Join(",", request.CategoryList));
            return (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_UPSERT_CATEGORY(request.UserId, (byte?)mode, string.Empty, string.Empty, null, categoryIdsObj).FirstOrDefault().GetValueOrDefault()).ConfigureAwait(false);
        }

        public async Task<SystemDbStatus> UpsertCategory(CategoryFriends request, int categoryId, SystemDbStatus mode)
        {
            var friends = request.Friends != null && request.Friends.Any() ? String.Join(",", request.Friends) : null;
            var result = (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_UPSERT_CATEGORY_FRIENDS(request.UserId, (byte?)mode, friends, categoryId).FirstOrDefault().GetValueOrDefault()).ConfigureAwait(false);
            return result;
        }

        //public SystemDbStatus CopyCategory(CopyCategory request, SystemDatabaseOperationMode operation)
        //{
        //    this.Context.PROC_UPSERT_USER_CATEGORY_FRIENDS(request.UserId, string.Join(",", request.Friends), request.CategoryId, string.Join(",", request.TargetCategories), Convert.ToBoolean(operation));
        //    return operation.Equals(SystemDatabaseOperationMode.Copy) ? SystemDbStatus.Inserted : SystemDbStatus.Updated;
        //}

        //public async Task<PaginatedResponse<IEnumerable<int>>> GetFriendsInCategoryList(int userId,
        //    int userCategoryTypeId, int? pageIndex, int? pageSize)
        //{
        //    var page = new PaginatedResponse<IEnumerable<int>>
        //    {
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        Page = await Task.Factory.StartNew(() => this.Context.UserCategoryFriends.Where(
        //            x => (x.UserID == userId && x.UserCategoryTypeID == userCategoryTypeId)).Select(x => x.FriendId))
        //    };

        //    if (pageIndex.HasValue && pageSize.HasValue)
        //    {
        //        page.Page = page.Page
        //            .Skip(pageIndex.Value * pageSize.Value)
        //            .Take(pageSize.Value + 1)
        //            .ToList();
        //        page.HasNextPage = page.Page.Count() > pageSize;
        //        page.Page = page.Page.Take(pageSize.Value);
        //    }

        //    return page;
        //}
    }
}
