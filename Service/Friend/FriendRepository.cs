using Model.Common;
using Model.Friend;
using Model.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Friend
{
    public class FriendRepository : IFriendRepository
    {
        protected readonly Services Client;

        public FriendRepository(Services client)
        {
            Client = client;
        }

        public async Task<StatusData<string>> SyncPhoneBook(PhoneBookContactsRequest request, SystemSession session)
        {
            var contacts = new UserMobileContacts
            {
                CTag = request.CTag,
                DeviceId = request.DeviceId,
                UserId = request.UserId,
                ToAddmobileContacts =
                    request.Add.Select(
                        x =>
                            new MobileContact
                            {
                                CountryCode = x.CountryCode,
                                PrimaryContactNumber = x.MobileNumber.ToString()
                            }).ToList(),
                ToDelmobileContacts =
                    request.Delete.Select(
                        x =>
                            new MobileContact
                            {
                                CountryCode = x.CountryCode,
                                PrimaryContactNumber = x.MobileNumber.ToString()
                            }).ToList(),
                Flush = request.Flush
            };
            var result = new StatusData<string>();
            var response = (await Task.Factory.StartNew(() => Client.UserService.contactSync(contacts, session.GetSession())).ConfigureAwait(false));
            result.Status = (SystemDbStatus)response.SystemDbStatus;
            result.Data = response.CTag;
            result.Message = response.DbStatusMsg;
            return result;
        }

        public async Task<StatusData<FriendInformationCategorizedResponse>> GetFriends(string deviceId, string cTag, SystemSession session)
        {
            //await Task.Delay(1000);
            var result = new StatusData<FriendInformationCategorizedResponse> { Status = SystemDbStatus.Selected, Data = new FriendInformationCategorizedResponse() };
            var response = await Task.Factory.StartNew(() =>
                Client.UserService.getFriendsListForMobile(session.UserId, deviceId, cTag, session.GetSession())).ConfigureAwait(false);

            //var response =
            //    _client.UserService.getFriendsListForMobile(userId, deviceId, cTag, session.GetSession());

            var allFriends = response.Friends.Select(item => new FriendInformationResponse
              {
                  UserId = Convert.ToInt32(item.User.UserId),
                  UserName = item.User.UserName,
                  FirstName = item.UserInfoPerson.FirstName,
                  LastName = item.UserInfoPerson.LastName,
                  CountryCode = item.Country.CountryCode,
                  MobileNumber = item.User.PrimaryContactNumber,
                  StatusTypeId = (SystemUserAvailabilityCode)(item.StatusType.StatusTypeId),
                  Title = item.UserInfoPerson.Title,
                  UserTypeId = (SystemUserType)item.UserInfo.UserTypeId,
                  Image = item.UserInfo.Picture,
                  FriendshipStatus = (SystemFriendshipStatus)(item.FriendshipStatus),
                  CanMessage = item.CanMessage,
                  AllowAddingInChatGroup = item.AllowAddingChatGroup,
                  AllowMessageForwarding = item.AllowMsgForword,
                  CanReceiveConnectionRequest = item.ReceiveConnectionRequest,
                  IsFromPhoneBook = item.FromPhoneBook,
                  Address = item.UserInfo.Address,
                  Email = item.User.Email
              }).ToList();

            //allFriends.ForEach(x => x.Image = ImageService.GetProfilePhoto(x.Image, (byte)x.UserTypeId));


            result.Data.CTag = response.CTag;
            result.Data.MobileContacts = allFriends.Where(x => x.IsFromPhoneBook);
            result.Data.NonMobileContacts = allFriends.Where(x => !x.IsFromPhoneBook);

            return result;
        }

        public async Task<StatusData<string>> RequestFriend(FriendshipRequest request, SystemSession session)
        {
            return (await Task.Factory.StartNew(() => Client.UserService.FriendRequest(session.UserName, request.FriendId, request.CategoryId, session.GetSession(), request.FriendId)).ConfigureAwait(false)).GetStatusData<string>();
        }

        public async Task<StatusData<string>> RespondFriendRequest(FriendRespondRequest request, SystemSession session)
        {
            var result = new StatusData<string>();
            var response = await Task.Factory.StartNew(() => Client.UserService.FriendResponse(session.UserName, request.FriendId, request.CategoryId, request.Accept, session.GetSession(), request.FriendId)).ConfigureAwait(false);
            result.Data = response.CTag;
            result.Status = (SystemDbStatus)response.DbStatusCode;
            result.Message = response.DbStatusMsg;
            return result;
        }

        public async Task<StatusData<string>> UnFriendRequest(FriendRequest request, SystemSession session)
        {
            var result = new StatusData<string>();
            var response = await Task.Factory.StartNew(() => Client.UserService.UnFriend(session.UserName, request.FriendId, session.GetSession(), request.FriendId)).ConfigureAwait(false);
            result.Data = response.CTag;
            result.Status = (SystemDbStatus)response.DbStatusCode;
            result.Message = response.DbStatusMsg;
            return result;
        }
    }
}
