using Model.Base;
using Model.Search;
using Model.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Search
{
    public class SearchRepository : ISearchRepository
    {
        protected readonly Services Client;

        public SearchRepository(Services client)
        {
            Client = client;
        }

        public async Task<UserSearchResponse> UserSearch(UserSearchRequest request, SystemSession session)
        {
            var serviceRequest = new UserSearchQuery
            {
                Query = request.Query,
                Cursor = request.Cursor,
                Limit = request.Limit
            };

            var response = await Task.Factory.StartNew(() => Client.SearchService.userSearch(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new UserSearchResponse
            {
                Cursor = response.Cursor,
                Limit = response.Limit,
                Count = response.Count,

                User = response.Users != null ? response.Users.Select(x => new UserInformationBaseExtendedResponse
                {
                    FirstName = x.UserInfoPerson.FirstName,
                    LastName = x.UserInfoPerson.LastName,
                    Image = x.UserInfo.Picture,
                    Title = x.UserInfo.Title,
                    UserId = Convert.ToInt32(x.UserInfoPerson.UserId),
                    UserName = x.UserName,
                    UserTypeId = (SystemUserType)x.UserInfo.UserTypeId
                }) : null
            };
            return result;
        }
    }
}
