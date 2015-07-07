using Model.Base;
using Model.Common;
using Model.Friend;
using Model.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Friend;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Sealed.Friend
{
    public sealed class FriendRepositorySealed : FriendRepository, IFriendRepositorySealed
    {
        public FriendRepositorySealed(Services client)
            : base(client)
        { }

        public async Task<PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>> GetMutualFriends(PaginatedRequest<string> request, SystemSession session)
        {
            var serviceRequest = new MutualFriend
            {
                UserId = session.UserId,
                TargetType = "F",
                TargetUser = request.Data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            var data = await Task.Factory.StartNew(() => Client.UserService.getMutualFriends(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = new PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>
            {
                Page = data.MutualFriends.Select(x => new UserInformationBaseExtendedResponse
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserTypeId = (SystemUserType)x.UserTypeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Image = x.Picture,
                    Title = x.Title
                }),
                HasNextPage = data.HasNextPage,
                Information = data.Count
            };

            return result;
        }

        public async Task<PaginatedResponse<IEnumerable<WebFriendInformationResponse>>> GetWebFriends(PaginatedRequest<string> request, SystemSession session)
        {
            var serviceRequest = new WebOnlyFriend
            {
                UserId = session.UserId,
                TargetUser = request.Data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            var data = await Task.Factory.StartNew(() => Client.UserService.getWebOnlyFriends(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = new PaginatedResponse<IEnumerable<WebFriendInformationResponse>>
            {
                Page = data.WebOnlyFriends.Select(x => new WebFriendInformationResponse
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    StatusTypeId = (SystemUserAvailabilityCode)x.Status,
                    MobileNumber = x.PrimaryContactNum,
                    Title = x.Title,
                    UserTypeId = (SystemUserType)x.UserTypeId,
                    Image = x.Picture,
                    FriendshipStatus = (SystemFriendshipStatus)x.IsConnected,
                    MutualFriend = x.MutualFriends,
                    Address = x.Address,
                    Email = x.Email,
                    CanMessage = x.CanMessage,
                    AllowMessageForwarding = x.AllowMsgForwarding,
                    CanReceiveConnectionRequest = x.CanReceiveConnectionReq,
                    AllowAddingInChatGroup = x.AllowAddingChatGroup
                }),
                HasNextPage = data.HasNextPage,
            };

            return result;
        }

    }
}
