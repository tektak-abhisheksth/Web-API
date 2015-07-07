using DAL.DbEntity;
using Entity;
using Model.Base;
using Model.Common;
using Model.Friend;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Friend
{
    public class FriendRepository : GenericRepository<Entity.Friend>, IFriendRepository
    {
        public FriendRepository(iLoopEntity context) : base(context) { }

        public async Task<StatusData<string>> SyncPhoneBook(PhoneBookContactsRequest request)
        {
            var cTag = new ObjectParameter("CTAG", default(Guid));
            var contactsAdd = ReformatContacts(request.Add);
            var contactsDelete = ReformatContacts(request.Delete);
            var result = new StatusData<string>();
            result.Status = (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_SYNC_PHONEBOOK(request.UserId, request.DeviceId, contactsAdd, contactsDelete, request.Flush, cTag).FirstOrDefault()).ConfigureAwait(false);
            result.Data = cTag.Value.ToString();
            return result;
        }

        public async Task<StatusData<FriendInformationCategorizedResponse>> GetFriends(int userId, string deviceId, string cTag)
        {
            var result = new StatusData<FriendInformationCategorizedResponse> { Status = SystemDbStatus.Selected, Data = new FriendInformationCategorizedResponse() };
            var user = await Context.UserLogins.FirstOrDefaultAsync(x => x.UserId == userId).ConfigureAwait(false);
            if (cTag != null && user.CTag.Equals(cTag))
            {
                result.Status = SystemDbStatus.NotModified;
                return result;
            }

            var allFriends = await Task.Factory.StartNew(() =>
                Context.FNGETADDRESSBOOKFRIENDS_MOB(userId, deviceId).Select(item => new FriendInformationResponse
                {
                    UserId = item.USERID,
                    UserName = item.USERNAME,
                    FirstName = item.FIRSTNAME,
                    LastName = item.LASTNAME,
                    CountryCode = item.COUNTRYCODE,
                    MobileNumber = item.PRIMARYCONTACTNUMBER,
                    StatusTypeId = (SystemUserAvailabilityCode)item.STATUS,
                    Title = item.TITLE,
                    UserTypeId = (SystemUserType)item.USERTYPEID,
                    Image = item.PICTURE,
                    FriendshipStatus = (SystemFriendshipStatus)item.ISCONNECTED,
                    CanMessage = item.CANMESSAGE,
                    AllowAddingInChatGroup = item.ALLOWADDINGINCHATGROUP,
                    AllowMessageForwarding = item.ALLOWMESSAGEFORWARDING,
                    CanReceiveConnectionRequest = item.CANRECEIVECONNECTIONREQUEST,
                    IsFromPhoneBook = item.ISFROMPHONEBOOK
                }).ToList()).ConfigureAwait(false);

            //allFriends.ForEach(x => x.Image = ImageRepository.GetProfilePhoto(x.Image, x.UserTypeId));

            result.Data.MobileContacts = allFriends.Where(x => x.IsFromPhoneBook);
            result.Data.NonMobileContacts = allFriends.Where(x => !x.IsFromPhoneBook);

            result.Data.CTag = user.CTag;

            return result;
        }

        public async Task<SystemDbStatus> RequestFriend(int userId, int friendId, int categoryId = 0)
        {
            var requestIdObj = new ObjectParameter("REQUESTID", 0);
            var requestTypeIdObj = new ObjectParameter("REQUESTTYPEID", 0);
            return (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_REQUEST_FRIEND(userId, friendId, categoryId, requestIdObj, requestTypeIdObj).FirstOrDefault().GetValueOrDefault()).ConfigureAwait(false);
        }

        //public async Task<StatusData<string>> RespondFriendRequest(int userId, int friendId, bool isAccepted = true, int categoryId = 0)
        //{
        //    var result = new StatusData<string>();
        //    var cTagObj = new ObjectParameter("NEWCTAG", Guid.NewGuid().ToString());
        //    result.Status = (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_RESPOND_FRIENDREQUEST(userId, friendId, categoryId, isAccepted, cTagObj).FirstOrDefault().GetValueOrDefault());
        //    result.Data = cTagObj.Value.ToString();
        //    return result;
        //}

        public async Task<StatusData<string>> UnFriendRequest(int userId, int friendId)
        {
            var result = new StatusData<string>();
            var cTagObj = new ObjectParameter("NEWCTAG", Guid.NewGuid().ToString());
            result.Status = (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_UNFRIEND(userId, friendId, cTagObj).FirstOrDefault().GetValueOrDefault()).ConfigureAwait(false);
            result.Data = cTagObj.Value.ToString();
            return result;
        }

        public async Task<PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>> MutualFriends(PaginatedRequest<string> request)
        {
            var hasNextPage = new ObjectParameter("HASNEXTPAGE", typeof(bool));
            var totalFriendCount = new ObjectParameter("TOTALRELATEDFRIENDS", typeof(int));
            var data = await Task.Factory.StartNew(() => Context.PROC_MUTUAL_FRIENDS(request.UserId, "F", request.Data, request.PageIndex, request.PageSize, hasNextPage, totalFriendCount).ToList()).ConfigureAwait(false);
            var result = new PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>
            {
                Page = data.Select(x => new UserInformationBaseExtendedResponse
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserTypeId = (SystemUserType)x.UserTypeID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Image = x.Picture,
                    Title = x.Title
                }),
                HasNextPage = Convert.ToBoolean(hasNextPage.Value),
                Information = Convert.ToInt32(totalFriendCount.Value)
            };

            return result;
        }

        #region Private helper methods
        private string ReformatContacts(IEnumerable<PhoneBookContact> contacts)
        {
            var contactsSb = new StringBuilder();
            contacts.GroupBy(c => c.CountryCode, m => m.MobileNumber,
                (key, value) => new { CountryCode = key, MobileNumbers = value })
                .Select(x => string.Concat(x.CountryCode, ",", string.Join(",", x.MobileNumbers.OrderBy(y => y))))
                .OrderBy(x => x)
                .ToList()
                .ForEach(x => contactsSb.Append(x).Append("|"));
            var contactsStr = contactsSb.ToString();
            return contactsStr.Length > 0 ? contactsStr.Substring(0, contactsSb.Length - 1) : string.Empty;
        }
        #endregion

        //public async Task<SystemDbStatus> UpdatePhoneBookContacts(PhoneBookContactsRequest request)
        //{
        //    foreach (var item in request.Add.Distinct().ToList())
        //    {
        //        var contact = await Context.UserMobileContacts.FirstOrDefaultAsync(x => x.UserID == request.UserId && x.DeviceID == request.DeviceId && x.CountryCode == item.CountryCode && item.MobileNumber.ToString() == x.PrimaryContactNumber);
        //        if (contact == null)
        //            Context.UserMobileContacts.Add(new UserMobileContact
        //            {
        //                CountryCode = item.CountryCode,
        //                DeviceID = request.DeviceId,
        //                PrimaryContactNumber = item.MobileNumber.ToString(),
        //                UserID = request.UserId
        //            });
        //    }
        //    return SystemDbStatus.Inserted;
        //}

        //public async Task<SystemDbStatus> RemovePhoneBookContacts(PhoneBookContactsRequest request)
        //{
        //    foreach (var item in request.Delete.Distinct())
        //    {
        //        var result = await Context.UserMobileContacts.FirstOrDefaultAsync(
        //                x =>
        //                    x.CountryCode == item.CountryCode && x.DeviceID == request.DeviceId &&
        //                    x.PrimaryContactNumber == item.MobileNumber.ToString() && x.UserID == request.UserId);
        //        if (result != null)
        //            Context.UserMobileContacts.Remove(result);
        //    }
        //    return SystemDbStatus.Deleted;
        //}

        //public async Task<StatusData<string>> RandomizeCtag(int userId)
        //{
        //    var cTag = Guid.NewGuid().ToString();
        //    var result = new StatusData<string> { Status = SystemDbStatus.Updated };
        //    result.Data = cTag;
        //    var user = await Context.UserLogins.FirstOrDefaultAsync(x => x.UserId == userId);
        //    user.CTag = cTag;
        //    return result;
        //}


        //public SystemDbStatus GetFriends(int userId, string deviceId, string cTag, ref FriendInformationCategorizedResponse data)
        //{
        //    var user = Context.UserLogins.FirstOrDefault(x => x.UserId == userId);
        //    if (user == null || user.CTag.Equals(cTag))
        //        return SystemDbStatus.NoContent;

        //    var nonFriendSystemusers =
        //        from c in Context.UserLogins.Where(x => !x.HasDeactivated && x.IsActivated && x.IsApproved)
        //        join p in
        //            Context.UserMobileContacts.Where(
        //                x => userId.Equals(x.UserID) && deviceId.Equals(x.DeviceID)) on
        //            new { c.PrimaryContactNumber, c.UserInfo.CountryCode } equals
        //            new { p.PrimaryContactNumber, p.CountryCode }
        //        select c;

        //    data = new FriendInformationCategorizedResponse();
        //    var allData = ((this.Context.Friends.Where(
        //        x =>
        //            x.UserID == userId
        //            ||
        //            this.Context.Friends.Where(y => y.FriendID == userId)
        //                .Select(z => z.UserID)
        //                .Contains(x.FriendID))
        //        .Select(
        //            x =>
        //                new FriendInformationResponse
        //                {
        //                    FirstName =
        //                        x.UserInfo1.UserTypeID == (byte)SystemUserType.Person
        //                            ? x.UserInfo1.UserInfoPerson.FirstName
        //                            : x.UserInfo1.UserInfoCompany.Name,
        //                    LastName = x.UserInfo1.UserTypeID == (byte)SystemUserType.Person
        //                        ? x.UserInfo1.UserInfoPerson.LastName
        //                        : null,
        //                    UserId = x.FriendID,
        //                    Image = x.UserInfo1.Picture,
        //                    UserTypeId = x.UserInfo1.UserTypeID,
        //                    Title = this.Context.FNGETUSERTITLE(x.FriendID, 1).FirstOrDefault().TITLE,
        //                    FriendshipStatus =
        //                        (byte)
        //                            this.Context.FNGETFRIENDSHIPSTATUS(userId, x.FriendID)
        //                                .FirstOrDefault()
        //                                .Value,
        //                    MobileNumber = x.UserInfo1.UserLogin.PrimaryContactNumber,
        //                    StatusTypeId = x.UserInfo1.Status.StatusTypeId
        //                })
        //        ).Union(
        //            nonFriendSystemusers.Select(
        //                x =>
        //                    new FriendInformationResponse
        //                    {
        //                        FirstName =
        //                            x.UserInfo.UserTypeID == (byte)SystemUserType.Person
        //                                ? x.UserInfo.UserInfoPerson.FirstName
        //                                : x.UserInfo.UserInfoCompany.Name,
        //                        LastName = x.UserInfo.UserTypeID == (byte)SystemUserType.Person
        //                            ? x.UserInfo.UserInfoPerson.LastName
        //                            : null,
        //                        UserId = x.UserId,
        //                        Image = x.UserInfo.Picture,
        //                        UserTypeId = x.UserInfo.UserTypeID,
        //                        Title = this.Context.FNGETUSERTITLE(x.UserId, 1).FirstOrDefault().TITLE,
        //                        FriendshipStatus =
        //                            (byte)
        //                                this.Context.FNGETFRIENDSHIPSTATUS(userId, x.UserId)
        //                                    .FirstOrDefault()
        //                                    .Value,
        //                        MobileNumber = x.UserInfo.UserLogin.PrimaryContactNumber,
        //                        StatusTypeId = x.UserInfo.Status.StatusTypeId
        //                    }
        //                )
        //        )).OrderBy(x => x.FirstName).ToList();

        //    foreach (var friendInformationResponse in allData.Where(x => x.UserTypeId == (byte)SystemUserType.Person))
        //    {
        //        var setDb =
        //             this.Context.SettingPersons.FirstOrDefault(x => x.UserId == friendInformationResponse.UserId && x.SettingTypeId == (byte)SystemSettingPerson.ReceiveMessages);
        //        switch ((SystemSettingValue)setDb.Value)
        //        {
        //            case SystemSettingValue.NoOne:
        //            case SystemSettingValue.Private:
        //                friendInformationResponse.CanMessage = false;
        //                break;
        //            case SystemSettingValue.Public:
        //            case SystemSettingValue.Friends:
        //                friendInformationResponse.CanMessage = true;
        //                break;
        //            case SystemSettingValue.Category:
        //                var lst =
        //                    Context.SettingReferencePersons.Where(x => x.ReferenceToken == setDb.ReferenceToken)
        //                        .Select(x => x.EntryList);
        //                friendInformationResponse.CanMessage = (this.Context.UserCategoryFriends.FirstOrDefault(y => y.UserID == friendInformationResponse.UserId && lst.Contains(y.UserCategoryTypeID) && y.FriendId == userId) != null);
        //                break;
        //            case SystemSettingValue.Contact:
        //                lst =
        //                 Context.SettingReferencePersons.Where(x => x.ReferenceToken == setDb.ReferenceToken)
        //                     .Select(x => x.EntryList);
        //                //set.Value = (byte)(lst.Contains(userId) ? SystemSettingValue.NoOne : SystemSettingValue.Friends);
        //                friendInformationResponse.CanMessage = lst.Contains(userId);
        //                break;
        //        }
        //    }

        //    data.Friends = allData.Where(x => x.FriendshipStatus == (byte)SystemFriendshipStatus.Friend);
        //    data.FriendRequestSent = allData.Where(x => x.FriendshipStatus == (byte)SystemFriendshipStatus.FriendRequestSent);
        //    data.FriendshipRequestReceived = allData.Where(x => x.FriendshipStatus == (byte)SystemFriendshipStatus.FriendshipRequestReceived);
        //    data.NotFriends = allData.Where(x => x.FriendshipStatus == (byte)SystemFriendshipStatus.NotFriend);

        //    data.Friends.ToList().ForEach(x => x.Image = ImageRepository.GetProfilePhoto(x.Image, x.UserTypeId));
        //    data.FriendRequestSent.ToList().ForEach(x => x.Image = ImageRepository.GetProfilePhoto(x.Image, x.UserTypeId));
        //    data.FriendshipRequestReceived.ToList().ForEach(x => x.Image = ImageRepository.GetProfilePhoto(x.Image, x.UserTypeId));
        //    data.NotFriends.ToList().ForEach(x => x.Image = ImageRepository.GetProfilePhoto(x.Image, x.UserTypeId));

        //    data.CTag = user.CTag;

        //    return SystemDbStatus.Selected;
        //}
    }
}
