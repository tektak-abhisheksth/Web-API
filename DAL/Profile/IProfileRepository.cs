using DAL.DbEntity;
using Entity;
using Model.Common;
using Model.Profile;
using Model.Profile.Personal;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Profile
{
    public interface IProfileRepository : IGenericRepository<UserInfo>
    {
        //Task<PersonalProfileResponse> GetProfileInformation(int userId, int targetId);
        Task<BaseInfoResponse> GetBasicUserInformation(int baseUserId, string userNameOrUserId);
        //Task<IEnumerable<TemporaryContactInformation>> GetContacts(int userId, string targetUser, byte? contactTypeId);
        //Task<List<Employment>> GetEmploymentHistory(int userId, int targetId);
        Task UpdateLocation(int userId, double latitude, double longitude, DateTimeOffset offset);
        //Task<StatusData<ContactResponse>> AddContact(ContactRequest request);
        //Task<StatusData<ContactResponse>> UpdateContact(UpdateContactRequest request);
        //Task<StatusData<string>> DeleteContact(SingleData<long> request);
        Task<StatusData<string>> FlushContact(int userId, SingleData<GeneralKvPair<long, List<int>>> chatNetworks, SingleData<GeneralKvPair<long, List<long>>> customContacts);
        //Task<StatusData<string>> UpdateMeta(int userId, UpdateUserChatNetworkRequest userChatNetworks, UpdateUserCustomContactRequest customContacts);
        //Task<StatusData<ContactResponse>> AddMeta(int userId, MetaChatNetworkRequest chatNetworks, MetaCustomContactNetwork customContacts);
        Task<ContactSuggestions> GetSuggestedContactList(int targetUserId);
        //Task<StatusData<ContactSuggestionResponse>> AddSuggestedContact(AddSuggestContactRequest request);
        //Task<StatusData<IEnumerable<long>>> UpdateSuggestedContact(UpdateSuggestContactRequest request);
        //Task<StatusData<string>> DeleteSuggestedContact(SingleData<GeneralKvPair<long, List<long>>> request);
        Task<StatusData<string>> SuggestedContactOperation(SuggestedContactOperationRequest request);
        Task<List<UserInformation>> GetProfiles(int userId, IEnumerable<GeneralKvPair<int, long>> profileTags);
        Task<List<TemporaryContactInformation>> GetProfilesContacts(int userId, IEnumerable<int> targetUsers);
        Task<PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>> GetProfileViewDetail(PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>> request);
        Task<InformationResponse<IEnumerable<ViewerPanelResponse>, int>> GetProfileViewPanel(int userId);
        Task<InformationResponse<IEnumerable<ViewSummaryResponse>, int>> GetProfileViewSummary(PaginatedRequest<int> request);
        Task<PaginatedResponse<IEnumerable<UserSkillResponse>>> GetUserSkills(PaginatedRequest<string> request);
    }
}
