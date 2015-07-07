using Model.Common;
using Model.Profile.Personal;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Profile
{
    public partial interface IProfilePersonalService
    {
        Task<BasicInformationWeb> GetBasicInformationWeb(int userId, string targetUser, SystemSession session);
        Task<IEnumerable<EducationViewResponse>> GetEducationHistory(string targetUser, SystemSession session);
        Task<StatusData<long>> InsertEducationHistory(AddAcademicRequest request, SystemSession session);
        Task<StatusData<string>> UpdateEducationHistory(UpdateAcademicRequest request, SystemSession session);
        Task<StatusData<string>> DeleteEducationHistory(SingleData<int> request, SystemSession session);
        Task<IEnumerable<LanguageResponse>> GetLanguage(string targetUser, SystemSession session);
        Task<StatusData<string>> UpdateLanguages(SingleData<List<LanguageRequest>> request, SystemSession session);
        Task<IEnumerable<AwardAndHonorResponse>> GetAwardAndHonor(string targetUser, SystemSession session);
        Task<StatusData<long>> InsertAwardAndHonor(AddAwardAndHonorRequest request, SystemSession session);
        Task<StatusData<string>> UpdateAwardAndHonor(UpdateAwardAndHonorRequest request, SystemSession session);
        Task<StatusData<string>> DeleteAwardAndHonor(SingleData<long> request, SystemSession session);
        Task<PaginatedResponse<IEnumerable<UserSkillResponse>>> GetUserSkills(PaginatedRequest<string> request, SystemSession session);
        Task<StatusData<string>> UpsertSkill(SkillRequest request, SystemSession session);
        Task<StatusData<string>> UpsertSuggestSkill(SkillSuggestionRequest request, SystemSession session);
        Task<StatusData<ThumbsForSkillResponse>> UpsertThumbsForSkill(ThumbsForSkillRequest request, SystemSession session);
        Task<PaginatedResponse<IEnumerable<ThumbsForSkillDetailResponse>>> GetThumbsForSkillDetail(PaginatedRequest<ThumbsForSkillDetailRequest> request, SystemSession session);
        Task<IEnumerable<UnApprovedSkillSuggestionResponse>> GetUnApprovedUserSkillSuggestion(string targetUser, SystemSession session);
        Task<StatusData<string>> AcceptSkill(SkillAcceptanceRequest request, SystemSession session);
        Task<IEnumerable<EmploymentWebResponse>> GetEmployment(string targetUser, SystemSession session);
        Task<IEnumerable<EmployeeWorkScheduleResponse>> GetEmploymentWorkSchedule(SingleData<long> request, SystemSession session);
        Task<StatusData<long>> AddEmployment(AddEmployeeRequest request, SystemSession session);

        Task<StatusData<string>> UpdateEmployment(UpdateEmployeeRequest request,
            SystemSession session);

        Task<StatusData<string>> DeleteEmployment(SingleData<int> request, SystemSession session);
    }
}
