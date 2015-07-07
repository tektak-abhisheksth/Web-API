using Model.Category;
using Model.Common;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly Services Client;

        public CategoryRepository(Services client)
        {
            Client = client;
        }

        public async Task<IEnumerable<CategoryResponse>> GetUserCategoryList(SystemSession session)
        {
            var result = await Task.Factory.StartNew(() => Client.UserService.categoryList(session.UserId, session.GetSession())).ConfigureAwait(false);
            return result.Select(c => new CategoryResponse
            {
                Name = c.Name,
                CategoryId = c.UserCategoryTypeId,
                Description = c.Description,
                // Users = c.Friends.ToList().Select(x => Convert.ToInt32(x)),
                Friends = c.Friends == null ? new List<int>() : c.Friends.ToList().Select(x => Convert.ToInt32(x)),
                IsSystemDefaultCategory = c.IsSystemDefault
            });
        }

        public async Task<StatusData<byte?>> InsertCategory(CategoryAddRequest request, SystemSession session)
        {
            var userCategory = new UserCategory { UserId = session.UserId.ToString(), Name = request.Name, Description = request.Description };
            var result = new StatusData<byte?>();
            var friends = request.Friends != null && request.Friends.Any() ? String.Join(",", request.Friends) : null;
            var serviceResponse = await Task.Factory.StartNew(() => Client.UserService.createNewCategory(userCategory, friends, session.GetSession())).ConfigureAwait(false);
            result.Data = (byte?)serviceResponse.UserCategoryTypeId;
            result.Message = serviceResponse.DbStatusMsg;
            result.Status = (SystemDbStatus)serviceResponse.DbStatusCode;
            result.SubStatus = serviceResponse.DbSubStatusCode;
            return result;
        }

        public async Task<StatusData<string>> UpdateCategory(CategoryUpdateRequest request, SystemSession session)
        {
            var userCategory = new UserCategory { UserId = session.UserId.ToString(), UserCategoryTypeId = request.CategoryId, Name = request.Name, Description = request.Description };
            var result = (await Task.Factory.StartNew(() => Client.UserService.editCategoryMeta(userCategory, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return result;
        }

        public async Task<StatusData<string>> DeleteCategory(DeleteCategory request, SystemSession session)
        {
            return (await Task.Factory.StartNew(() => Client.UserService.deleteMultipleCategories(request.UserId, request.CategoryList, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
        }

        public async Task<StatusData<string>> UpsertCategoryFriends(CategoryFriends request, SystemDbStatus mode, SystemSession session)
        {
            var userCategory = new UserCategory { UserId = session.UserId.ToString(), UserCategoryTypeId = request.CategoryId };
            var friends = request.Friends != null && request.Friends.Any() ? String.Join(",", request.Friends) : null;
            var result = (mode == SystemDbStatus.Inserted
                        ? await
                            Task.Factory.StartNew(
                                () => Client.UserService.insertInCategory(userCategory, friends, session.GetSession())).ConfigureAwait(false)
                        : await
                            Task.Factory.StartNew(
                                () => Client.UserService.removeFromCategory(userCategory, friends, session.GetSession())).ConfigureAwait(false));
            return result.GetStatusData<string>();
        }
    }
}
