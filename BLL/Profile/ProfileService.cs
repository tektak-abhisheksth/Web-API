using DAL.DbEntity;
using Model.Common;
using Model.Profile;
using Model.Profile.Personal;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Profile
{
    public partial class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public ProfileService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public async Task<List<ProfileOfflineResponse>> GetProfiles(IEnumerable<GeneralKvPair<int, long>> profileTags, SystemSession session)
        {
            var response = new List<ProfileOfflineResponse>();
            var basicInfo = await _unitOfWork.Profile.GetProfiles(session.UserId, profileTags).ConfigureAwait(false);

            if (basicInfo.Any())
            {
                var contactInfoTask = _unitOfWork.Profile.GetProfilesContacts(session.UserId, basicInfo.Select(x => x.UserId)).ConfigureAwait(false);
                response.AddRange(basicInfo.Select(x => new ProfileOfflineResponse { BasicInformation = x }));
                var contactInfo = await contactInfoTask;
                foreach (var profileOfflineResponse in response)
                {
                    var home = contactInfo.Where(x => x.UserId == profileOfflineResponse.BasicInformation.UserId).FirstOrDefault(x => x.ContactTypeId == SystemContactType.Home);
                    var office = contactInfo.Where(x => x.UserId == profileOfflineResponse.BasicInformation.UserId).FirstOrDefault(x => x.ContactTypeId == SystemContactType.Office);
                    profileOfflineResponse.Contacts = new Contact
                    {
                        Home = home == null ? null : home.GetParent(),
                        Office = office == null ? null : office.GetParent(),
                        Temporary = contactInfo.Where(x => x.UserId == profileOfflineResponse.BasicInformation.UserId).Where(x => x.ContactTypeId != SystemContactType.Home && x.ContactTypeId != SystemContactType.Office)
                    };
                }
            }

            return response;
        }

        public async Task<PersonalProfileResponse> GetProfileInformation(string targetUser, SystemSession session)
        {
            var profileInfo = await _jUnitOfWork.Profile.GetProfileInformation(targetUser, session).ConfigureAwait(false);
            var contactInfo = await _jUnitOfWork.Profile.GetContacts(targetUser, true, null, session).ConfigureAwait(false);

            //var contactInfo = await _unitOfWork.Profile.GetContacts(userId, targetUser, null).ConfigureAwait(false);
            //var profileInfo = await profileInfoTask;
            //var contactInfo = await contactInfoTask;
            //var employmentHistoryInfoTask = _unitOfWork.Profile.GetEmployment(userId, profileInfo.BasicInformation.UserId);

            //var employmentHistoryInfo = await employmentHistoryInfoTask.ConfigureAwait(false);

            var employmentHistoryInfo = await _jUnitOfWork.Profile.GetEmploymentHistory(targetUser, session).ConfigureAwait(false);
            var contactInformations = contactInfo as TemporaryContactInformation[] ?? contactInfo.ToArray();
            var home = contactInformations.FirstOrDefault(x => x.ContactTypeId == SystemContactType.Home);
            var office = contactInformations.FirstOrDefault(x => x.ContactTypeId == SystemContactType.Office);
            profileInfo.Contacts = new Contact
            {
                Home = home == null ? null : home.GetParent(),
                Office = office == null ? null : office.GetParent(),
                Temporary = contactInformations.Where(x => x.ContactTypeId != SystemContactType.Home && x.ContactTypeId != SystemContactType.Office)
            };
            profileInfo.EmploymentHistory = employmentHistoryInfo;
            return profileInfo;
        }

        public Task<BaseInfoResponse> GetBasicUserInformation(string userNameOrUserId)
        {
            // return _unitOfWork.Profile.GetBasicUserInformation(baseUserId, userNameOrUserId);
            return _jUnitOfWork.Profile.GetBasicUserInformation(userNameOrUserId);
        }

        public Task<IEnumerable<BaseInfoResponse>> GetBasicUsersInformation(SingleData<List<string>> request, SystemSession session)
        {
            // return _unitOfWork.Profile.GetBasicUserInformation(baseUserId, userNameOrUserId);
            return _jUnitOfWork.Profile.GetBasicUsersInformation(request, session);
        }

        public Task UpdateLocation(int userId, double latitude, double longitude, DateTimeOffset offset)
        {
            return _unitOfWork.Profile.UpdateLocation(userId, latitude, longitude, offset);
        }

        public Task<StatusData<ContactResponse>> AddContact(ContactRequest request, SystemSession session)
        {
            //return _unitOfWork.Profile.AddContact(request);
            return _jUnitOfWork.Profile.AddContact(request, session);
        }

        public Task<StatusData<ContactResponse>> UpdateContact(UpdateContactRequest request, SystemSession session)
        {
            // return _unitOfWork.Profile.UpdateContact(request);
            return _jUnitOfWork.Profile.UpdateContact(request, session);
        }

        public Task<StatusData<string>> DeleteContact(SingleData<long> request, SystemSession session)
        {
            //return _unitOfWork.Profile.DeleteContact(request);
            return _jUnitOfWork.Profile.DeleteContact(request, session);
        }

        public Task<StatusData<string>> FlushContact(SingleData<GeneralKvPair<long, List<int>>> chatNetworks, SingleData<GeneralKvPair<long, List<long>>> customContacts, SystemSession session)
        {
            // return _unitOfWork.Profile.FlushContact(userId, chatNetworks, customContacts);
            return _jUnitOfWork.Profile.FlushContact(chatNetworks, customContacts, session);
        }

        //public Task<StatusData<string>> UpdateMeta(int userId, UpdateUserChatNetworkRequest userChatNetworks, UpdateUserCustomContactRequest customContacts)
        //{
        //    return _unitOfWork.Profile.UpdateMeta(userId, userChatNetworks, customContacts);
        //}

        //public Task<StatusData<ContactResponse>> AddMeta(int userId, MetaChatNetworkRequest chatNetworks, MetaCustomContactNetwork customContacts)
        //{
        //    return _unitOfWork.Profile.AddMeta(userId, chatNetworks, customContacts);
        //}

        public Task<ContactSuggestions> GetSuggestedContactList(SystemSession session)
        {
            // return _unitOfWork.Profile.GetSuggestedContactList(targetUserId);
            return _jUnitOfWork.Profile.GetSuggestedContactList(session);
        }

        //public Task<StatusData<ContactSuggestionResponse>> AddSuggestedContact(AddSuggestContactRequest request)
        //{
        //    return _unitOfWork.Profile.AddSuggestedContact(request);
        //}

        public Task<StatusData<ContactResponse>> UpdateSuggestedContact(UpdateSuggestContactRequest request, SystemSession session)
        {
            //return _unitOfWork.Profile.UpdateSuggestedContact(request);
            return _jUnitOfWork.Profile.UpdateSuggestedContact(request, session);
        }

        public Task<StatusData<string>> DeleteSuggestedContact(SingleData<GeneralKvPair<long, List<long>>> request, SystemSession session)
        {
            // return _unitOfWork.Profile.DeleteSuggestedContact(request);
            return _jUnitOfWork.Profile.DeleteSuggestedContact(request, session);
        }

        public Task<StatusData<string>> SuggestedContactOperation(SuggestedContactOperationRequest request, SystemSession session)
        {
            //return _unitOfWork.Profile.SuggestedContactOperation(request);
            return _jUnitOfWork.Profile.SuggestedContactOperation(request, session);
        }
        public Task<StatusData<bool>> SaveProfilePicture(string username, SingleData<string> request)
        {
            return _jUnitOfWork.Profile.SaveProfilePicture(username, request);
        }

        public Task<StatusData<string>> UpdateBasicContactPerson(BasicContactPersonRequest request, SystemSession session)
        {
            return _jUnitOfWork.Profile.UpdateBasicContactPerson(request, session);
        }

        public Task<IEnumerable<ContactSettingResponse>> GetContactSettings(long contactId, SystemSession session)
        {
            return _jUnitOfWork.Profile.GetContactSettings(contactId, session);
        }

        public Task<IEnumerable<ContactSettingCategoriesResponse>> GetContactSettingCategory(ContactSettingCategoriesRequest request, SystemSession session)
        {
            return _jUnitOfWork.Profile.GetContactSettingCategory(request, session);
        }

        public Task<IEnumerable<ContactSettingFriendsResponse>> GetContactSettingFriend(ContactSettingCategoriesRequest request, SystemSession session)
        {
            return _jUnitOfWork.Profile.GetContactSettingFriend(request, session);
        }

        public Task<StatusData<string>> UpsertContactSetting(ContactSettingRequest request, byte mode, SystemSession session)
        {
            return _jUnitOfWork.Profile.UpsertContactSetting(request, mode, session);
        }

        public Task<UserStatusResponse> GetUserAvailability(string targetUser, SystemSession session)
        {
            return _jUnitOfWork.Profile.GetUserAvailability(targetUser, session);
        }

        public Task<StatusData<string>> UpdateUserAvailability(StatusSetRequest request, SystemSession session)
        {
            return _jUnitOfWork.Profile.UpdateUserAvailability(request, session);
        }

        public Task<StatusData<string>> SignalView(SignalViewRequest request, SystemSession session)
        {
            return _jUnitOfWork.Profile.SignalView(request, session);
        }
    }
}
