using Model.Common;
using Model.Profile.Personal;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Profile
{
    public partial class ProfilePersonalService
    {
        public Task<BasicInformationWeb> GetBasicInformationWeb(int userId, string targetUser, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetBasicInformationWeb(userId, targetUser, session);
        }

        public virtual Task<StatusData<string>> UpdateBasicContactPerson(BasicContactPersonWebRequest request,
            SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpdateBasicContactPerson(request, session);
        }

        public Task<IEnumerable<EducationViewResponse>> GetEducationHistory(string targetUser, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetEducationHistory(targetUser, session);
        }
        public Task<StatusData<long>> InsertEducationHistory(AddAcademicRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.InsertEducationHistory(request, session);
        }

        public Task<StatusData<string>> UpdateEducationHistory(UpdateAcademicRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpdateEducationHistory(request, session);
        }

        public Task<StatusData<string>> DeleteEducationHistory(SingleData<int> request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.DeleteEducationHistory(request, session);
        }

        public Task<IEnumerable<LanguageResponse>> GetLanguage(string targetUser, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetLanguage(targetUser, session);
        }

        public Task<StatusData<string>> UpdateLanguages(SingleData<List<LanguageRequest>> request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpsertLanguage(request, session);
        }

        public Task<IEnumerable<AwardAndHonorResponse>> GetAwardAndHonor(string targetUser, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetAwardAndHonor(targetUser, session);
        }

        public Task<StatusData<long>> InsertAwardAndHonor(AddAwardAndHonorRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.InsertAwardAndHonor(request, session);
        }

        public Task<StatusData<string>> UpdateAwardAndHonor(UpdateAwardAndHonorRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpdateAwardAndHonor(request, session);
        }

        public Task<StatusData<string>> DeleteAwardAndHonor(SingleData<long> request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.DeleteAwardAndHonor(request, session);
        }

        public Task<PaginatedResponse<IEnumerable<UserSkillResponse>>> GetUserSkills(PaginatedRequest<string> request, SystemSession session)
        {
            // return _unitOfWork.Profile.GetUserSkills(request);
            return _jUnitOfWork.ProfilePersonal.GetUserSkills(request, session);
        }

        public Task<StatusData<string>> UpsertSkill(SkillRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpsertSkill(request, session);
        }

        public Task<StatusData<string>> UpsertSuggestSkill(SkillSuggestionRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpsertSuggestSkill(request, session);
        }

        public Task<StatusData<ThumbsForSkillResponse>> UpsertThumbsForSkill(ThumbsForSkillRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpsertThumbsForSkill(request, session);
        }

        public Task<PaginatedResponse<IEnumerable<ThumbsForSkillDetailResponse>>> GetThumbsForSkillDetail(PaginatedRequest<ThumbsForSkillDetailRequest> request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetThumbsForSkillDetail(request.Data, request.PageIndex, request.PageSize, session);
        }

        public Task<IEnumerable<UnApprovedSkillSuggestionResponse>> GetUnApprovedUserSkillSuggestion(string targetUser, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetUnApprovedUserSkillSuggestion(targetUser, session);
        }

        public Task<StatusData<string>> AcceptSkill(SkillAcceptanceRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.AcceptSkill(request, session);
        }

        public Task<IEnumerable<EmploymentWebResponse>> GetEmployment(string targetUser, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetEmployment(targetUser, session);
        }

        public Task<IEnumerable<EmployeeWorkScheduleResponse>> GetEmploymentWorkSchedule(SingleData<long> request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.GetEmploymentWorkSchedule(request, session);
        }

        public Task<StatusData<long>> AddEmployment(AddEmployeeRequest request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.AddEmployment(request, session);
        }

        public Task<StatusData<string>> UpdateEmployment(UpdateEmployeeRequest request,
            SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.UpdateEmployment(request, session);
        }

        public Task<StatusData<string>> DeleteEmployment(SingleData<int> request, SystemSession session)
        {
            return _jUnitOfWork.ProfilePersonal.DeleteEmployment(request, session);
        }
    }
}
