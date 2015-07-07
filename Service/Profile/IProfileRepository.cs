using Model.Common;
using Model.Profile;
using Model.Profile.Personal;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TekTak.iLoop.Profile
{
    public interface IProfileRepository
    {
        Task<PersonalProfileResponse> GetProfileInformation(string targetUser, SystemSession session);
        Task<BaseInfoResponse> GetBasicUserInformation(string targetUser);
        Task<StatusData<string>> UpdateBasicContactPerson(BasicContactPersonRequest request, SystemSession session);
        Task<IEnumerable<TemporaryContactInformation>> GetContacts(string targetUser, bool allResults, long? displayOnlyContactId, SystemSession session);
        Task<IEnumerable<EmploymentHistoryResponse>> GetEmploymentHistory(string targetUser, SystemSession session);
        Task<ContactSuggestions> GetSuggestedContactList(SystemSession session);
        Task<StatusData<ContactSuggestionResponse>> AddSuggestedContact(AddSuggestContactRequest request, SystemSession session);
        Task<StatusData<ContactResponse>> UpdateSuggestedContact(UpdateSuggestContactRequest request, SystemSession session);
        Task<StatusData<string>> DeleteSuggestedContact(SingleData<GeneralKvPair<long, List<long>>> request, SystemSession session);
        Task<StatusData<string>> SuggestedContactOperation(SuggestedContactOperationRequest request, SystemSession session);
        Task<StatusData<ContactResponse>> AddContact(ContactRequest request, SystemSession session);
        Task<StatusData<ContactResponse>> UpdateContact(UpdateContactRequest request, SystemSession session);
        Task<StatusData<string>> DeleteContact(SingleData<long> request, SystemSession session);
        Task<StatusData<string>> FlushContact(SingleData<GeneralKvPair<long, List<int>>> chatNetworks, SingleData<GeneralKvPair<long, List<long>>> customContacts, SystemSession session);
        Task<IEnumerable<ContactSettingResponse>> GetContactSettings(long contactId, SystemSession session);
        Task<IEnumerable<ContactSettingCategoriesResponse>> GetContactSettingCategory(ContactSettingCategoriesRequest request, SystemSession session);
        Task<IEnumerable<ContactSettingFriendsResponse>> GetContactSettingFriend(ContactSettingCategoriesRequest request, SystemSession session);
        Task<StatusData<string>> UpsertContactSetting(ContactSettingRequest request, byte mode, SystemSession session);
        Task<StatusData<bool>> SaveProfilePicture(string username, SingleData<string> request);
        Task<IEnumerable<EmployeeWorkScheduleResponse>> GetEmploymentWorkSchedule(SingleData<long> request, SystemSession session);
        Task<StatusData<string>> RespondEmploymentRequest(RespondEmploymentRequest request, SystemSession session);
        Task<IEnumerable<BaseInfoResponse>> GetBasicUsersInformation(SingleData<List<string>> request, SystemSession session);
        Task<UserStatusResponse> GetUserAvailability(string targetUser, SystemSession session);
        Task<StatusData<string>> UpdateUserAvailability(StatusSetRequest request, SystemSession session);
        Task<StatusData<string>> SignalView(SignalViewRequest request, SystemSession session);
    }
}
