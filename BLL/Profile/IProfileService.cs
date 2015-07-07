using Model.Common;
using Model.Profile;
using Model.Profile.Personal;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Profile
{
    public partial interface IProfileService
    {
        Task<PersonalProfileResponse> GetProfileInformation(string targetUser, SystemSession session);
        //void SaveProfileInfo(string profileInfo);
        Task<BaseInfoResponse> GetBasicUserInformation(string userNameOrUserId);
        Task UpdateLocation(int userId, double latitude, double longitude, DateTimeOffset offset);
        Task<StatusData<ContactResponse>> AddContact(ContactRequest request, SystemSession session);
        Task<StatusData<ContactResponse>> UpdateContact(UpdateContactRequest request, SystemSession session);
        Task<StatusData<string>> DeleteContact(SingleData<long> request, SystemSession session);
        Task<StatusData<string>> FlushContact(SingleData<GeneralKvPair<long, List<int>>> chatNetworks, SingleData<GeneralKvPair<long, List<long>>> customContacts, SystemSession session);
        //Task<StatusData<string>> UpdateMeta(int userId, UpdateUserChatNetworkRequest userChatNetworks, UpdateUserCustomContactRequest customContacts);
        //Task<StatusData<ContactResponse>> AddMeta(int userId, MetaChatNetworkRequest chatNetworks, MetaCustomContactNetwork customContacts);
        Task<ContactSuggestions> GetSuggestedContactList(SystemSession session);
        //Task<StatusData<ContactSuggestionResponse>> AddSuggestedContact(AddSuggestContactRequest request);
        Task<StatusData<ContactResponse>> UpdateSuggestedContact(UpdateSuggestContactRequest request, SystemSession session);
        Task<StatusData<string>> DeleteSuggestedContact(SingleData<GeneralKvPair<long, List<long>>> request, SystemSession session);
        Task<StatusData<bool>> SaveProfilePicture(string username, SingleData<string> request);
        Task<StatusData<string>> SuggestedContactOperation(SuggestedContactOperationRequest request, SystemSession session);
        Task<StatusData<string>> UpdateBasicContactPerson(BasicContactPersonRequest request, SystemSession session);

        Task<IEnumerable<ContactSettingResponse>> GetContactSettings(long contactId,
            SystemSession session);

        Task<IEnumerable<ContactSettingCategoriesResponse>> GetContactSettingCategory(ContactSettingCategoriesRequest request, SystemSession session);
        Task<IEnumerable<ContactSettingFriendsResponse>> GetContactSettingFriend(ContactSettingCategoriesRequest request, SystemSession session);
        Task<StatusData<string>> UpsertContactSetting(ContactSettingRequest request, byte mode, SystemSession session);
        Task<List<ProfileOfflineResponse>> GetProfiles(IEnumerable<GeneralKvPair<int, long>> profileTags, SystemSession session);
        Task<IEnumerable<BaseInfoResponse>> GetBasicUsersInformation(SingleData<List<string>> request, SystemSession session);
        Task<StatusData<string>> SignalView(SignalViewRequest request, SystemSession session);
    }
}
