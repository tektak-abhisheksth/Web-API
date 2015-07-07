using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Category;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Sealed.Category
{
    public sealed class CategoryRepositorySealed : CategoryRepository, ICategoryRepositorySealed
    {
        public CategoryRepositorySealed(Services client)
            : base(client)
        { }

        public async Task<StatusData<string>> CategoryFriendsCopy(CopyCategory request, SystemSession session)
        {
            var userCategory = new UserCategory { UserId = session.UserId.ToString() };
            var friends = string.Join(",", request.Friends);
            var result = new StatusData<string>();
            var isOperationSuccessful = true;
            foreach (var categoryId in request.TargetCategories)
            {
                userCategory.UserCategoryTypeId = categoryId;
                var serviceResponse = await Task.Factory.StartNew(() => Client.UserService.insertInCategory(userCategory, friends, session.GetSession())).ConfigureAwait(false);
                isOperationSuccessful &= ((SystemDbStatus)serviceResponse.DbStatusCode).IsOperationSuccessful();
            }
            if (request.RemoveFromSource)
            {
                userCategory.UserCategoryTypeId = request.CategoryId;
                var serviceResponse = await Task.Factory.StartNew(() => Client.UserService.removeFromCategory(userCategory, friends, session.GetSession())).ConfigureAwait(false);
                isOperationSuccessful &= ((SystemDbStatus)serviceResponse.DbStatusCode).IsOperationSuccessful();
            }

            result.Status = isOperationSuccessful ? SystemDbStatus.Inserted : SystemDbStatus.Duplicate;
            return result;
        }

        public async Task<PaginatedResponse<IEnumerable<FriendsInCategoryResponse>>> GetFriendsInCategory(PaginatedRequest<FriendsInCategoryRequest> request, SystemSession session)
        {
            var serviceRequest = new UserCategory
            {
                UserId = session.UserId.ToString(),
                UserCategoryTypeId = request.Data.CategoryId,
                InvertCatSerch = request.Data.InvertCategorySearch,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize + 1
            };

            var data = await Task.Factory.StartNew(() => Client.UserService.getUserFriendsInCategory(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = new PaginatedResponse<IEnumerable<FriendsInCategoryResponse>>
            {
                Page = data.Select(x => new FriendsInCategoryResponse
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    Image = x.Picture,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Title = x.Title,
                    UserTypeId = (SystemUserType)x.UserTypeId,
                    Email = x.Email
                }).Take(request.PageSize),
                HasNextPage = data.Count > request.PageSize
            };

            return result;
        }
    }
}
