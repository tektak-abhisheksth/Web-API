using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Common;
using Model.Search;
using Model.Types;

namespace DAL.Search
{
    public class SearchRepository : GenericRepository<UserLogin>, ISearchRepository
    {
        public SearchRepository(iLoopEntity context) : base(context) { }
        public async Task<PaginatedResponse<IEnumerable<BasicSearchResponse>>> BasicSearch(int userId, string deviceId, string searchTerm, byte? userTypeId, byte? isConnected, int pageIndex, int pageSize)
        {
            await Task.Delay(1).ConfigureAwait(false);
            var hasNextPage = new ObjectParameter("HASNEXTPAGE", typeof(bool));
            var pg = Context.PROC_BASIC_SEARCH(userId, deviceId, searchTerm, userTypeId, isConnected, pageIndex, (pageSize + 1), hasNextPage).ToList();
            var page = new PaginatedResponse<IEnumerable<BasicSearchResponse>>
            {
                Page = pg
                    .Select(x => new BasicSearchResponse
                    {
                        UserId = x.USERID,
                        UserTypeId = (SystemUserType)x.USERTYPEID,
                        FirstName = x.FIRSTNAME,
                        LastName = x.LASTNAME,
                        Title = x.TITLE,
                        CountryCode = x.COUNTRYCODE,
                        FriendshipStatus = (SystemFriendshipStatus)x.ISCONNECTED,
                        MutualFriends = x.MUTUALFRIENDS,
                        StatusTypeId = (SystemUserAvailabilityCode)x.STATUS,
                        Image = x.PICTURE,
                        UserName = x.USERNAME,
                        AllowAddingInChatGroup = x.ALLOWADDINGINCHATGROUP,
                        AllowMessageForwarding = x.ALLOWMESSAGEFORWARDING,
                        CanMessage = x.CANMESSAGE,
                        CanReceiveConnectionRequest = x.CANRECEIVECONNECTIONREQUEST,
                        IsFromPhoneBook = x.ISFROMPHONEBOOK,
                        MobileNumber = x.PRIMARYCONTACTNUMBER
                    })
            };

            page.HasNextPage = Convert.ToBoolean(hasNextPage.Value);
            page.Page = page.Page.Take(pageSize);
            return page;
        }
    }
}
