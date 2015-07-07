using Model.Attribute;
using Model.Common;
using Model.Profile.Personal;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Profile
{
    public partial class ProfilePersonalController
    {
        /// <summary>
        /// Allows current user to update existing basic information.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-25", markType: 1, aliasName: "Profile_ContactBasic")]
        [ActionName("ContactBasic")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ContactBasicUpdate([FromBody] BasicContactPersonWebRequest request)
        {
            var response = await _profilePersonalService.UpdateBasicContactPerson(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with the list of his/her award and honor.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The list of award and honor.</returns>
        [HttpGet]
        [MetaData("2015-06-04", markType: 1, aliasName: "ProfilePersonal_GetAwardAndHonor")]
        [ActionName("AwardAndHonor"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/AwardAndHonor")]
        [ResponseType(typeof(IEnumerable<AwardAndHonorResponse>))]
        public async Task<HttpResponseMessage> GetAwardAndHonor(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
              && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetAwardAndHonor(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to add his/her award and honor information.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The award and honor ID of the information.</returns>
        [HttpPut]
        [MetaData("2015-06-04", markType: 1, aliasName: "ProfilePersonal_AddAwardAndHonor")]
        [ActionName("AwardAndHonor"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/AwardAndHonor")]
        [ResponseType(typeof(long))]
        public async Task<HttpResponseMessage> AddAwardAndHonor([FromBody] AddAwardAndHonorRequest request)
        {
            var response = await _profilePersonalService.InsertAwardAndHonor(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data });
        }

        /// <summary>
        /// Allows user to update his/her award and honor information.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-04", markType: 1, aliasName: "ProfilePersonal_UpdateAwardAndHonor")]
        [ActionName("AwardAndHonor"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/AwardAndHonor")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UpdateAwardAndHonor([FromBody] UpdateAwardAndHonorRequest request)
        {
            var response = await _profilePersonalService.UpdateAwardAndHonor(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to delete his/her award and honor information.
        /// </summary>
        /// <param name="request">The unique key representing the award and honor of the user.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData("2015-06-04", markType: 1, aliasName: "ProfilePersonal_DeleteAwardAndHonor")]
        [ActionName("AwardAndHonor"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/AwardAndHonor")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> DeleteAwardandHonor([FromBody] SingleData<long> request)
        {
            var response = await _profilePersonalService.DeleteAwardAndHonor(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with the basic profile information of the requested target user.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system, the unique user name chosen by the user, or the unique email address of the user.</param>
        /// <returns>Basic profile information of the user.</returns>
        [HttpGet]
        [MetaData("2015-06-04", markType: 1, aliasName: "ProfilePersonal_GetBasic")]
        [ActionName("Basic"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Basic")]
        [ResponseType(typeof(BasicInformationWeb))]
        public async Task<HttpResponseMessage> GetBasic(string targetUser)
        {
            var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);

            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
                 && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
                  && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState)
                 && !(targetUser.IsMatch(x => targetUser, RegexPattern.Email, ActionContext, ModelState)))
                return ActionContext.Response;

            if (!Validation.Required(targetUser, x => targetUser, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetBasicInformationWeb(userId, targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with his/her education history.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The education history of the user.</returns>
        [HttpGet]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_GetEducation")]
        [ActionName("Education"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Education")]
        [ResponseType(typeof(IEnumerable<EducationViewResponse>))]
        public async Task<HttpResponseMessage> GetEducationHistory(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
             && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
             && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetEducationHistory(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to add his/her education history.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The academic ID of the education information.</returns>
        [HttpPut]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_AddEducation")]
        [ResponseType(typeof(long))]
        [ActionName("Education"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Education")]
        public async Task<HttpResponseMessage> AddEducationHistory([FromBody] AddAcademicRequest request)
        {
            var response = await _profilePersonalService.InsertEducationHistory(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data });
        }

        /// <summary>
        /// Allows user to update his/her education history.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_UpdateEducation")]
        [ActionName("Education"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Education")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UpdateEducationHistory([FromBody] UpdateAcademicRequest request)
        {
            var response = await _profilePersonalService.UpdateEducationHistory(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to delete his/her education history.
        /// </summary>
        /// <param name="request">The academic ID of the user.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_DeleteEducation")]
        [ActionName("Education"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Education")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> DeleteEducationHistory([FromBody] SingleData<int> request)
        {
            var response = await _profilePersonalService.DeleteEducationHistory(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with the list of his/her languages.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The list of languages.</returns>
        [HttpGet]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_GetLanguage")]
        [ActionName("Language"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Language")]
        [ResponseType(typeof(IEnumerable<LanguageResponse>))]
        public async Task<HttpResponseMessage> GetLanguage(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
              && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetLanguage(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts a list of languages with optional proficiency levels. This will effectively flush all existing langauge information previously saved.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_UpdateLanguage")]
        [ActionName("Language"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Language")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UpdateLanguages(SingleData<List<LanguageRequest>> request)
        {
            var response = await _profilePersonalService.UpdateLanguages(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with the list of given user's skills.
        /// </summary>
        /// <param name="request">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The list of provided user's skills.</returns>
        [HttpPost]
        [MetaData("2015-06-12", markType: 1, aliasName: "ProfilePersonal_GetSkills")]
        [ActionName("Skill"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Skill")]
        [ResponseType(typeof(PaginatedResponse<IEnumerable<UserSkillResponse>>))]
        public async Task<HttpResponseMessage> GetSkill(PaginatedRequest<string> request)
        {
            if (!(request.Data.IsMatch(x => request.Data, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(request.Data, x => request.Data, 6, 30, ActionContext, ModelState))
              && !request.Data.IsMatch(x => request.Data, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetUserSkills(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to add/edit/delete skills.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData("2015-06-08", markType: 1, aliasName: "ProfilePersonal_UpdateSkills")]
        [ActionName("Skill"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Skills")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UpsertSkill([FromBody] SkillRequest request)
        {
            var response = await _profilePersonalService.UpsertSkill(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to like other user's skill.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The thumbs information.</returns>
        [HttpPost]
        [MetaData("2015-06-08", markType: 1, aliasName: "ProfilePersonal_AddSkillThumbsUp")]
        [ActionName("SkillThumbsUp"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/SkillThumbsUp")]
        [ResponseType(typeof(ThumbsForSkillResponse))]
        public async Task<HttpResponseMessage> SkillThumbsUp([FromBody] ThumbsForSkillRequest request)
        {
            if (!(request.ProfileUser.IsMatch(x => request.ProfileUser, RegexPattern.UserName, ActionContext, ModelState)
             && Validation.StringLength(request.ProfileUser, x => request.ProfileUser, 6, 30, ActionContext, ModelState))
             && !request.ProfileUser.IsMatch(x => request.ProfileUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.UpsertThumbsForSkill(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with the list of skill thumbs up/down provider.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The list of skill thumbs up/down provider.</returns>
        [HttpPost]
        [MetaData("2015-06-08", markType: 1, aliasName: "ProfilePersonal_GetSkillThumbsUp")]
        [ActionName("SkillThumbsProvider"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/SkillThumbsProvider")]
        [ResponseType(typeof(PaginatedResponse<IEnumerable<ThumbsForSkillDetailResponse>>))]
        public async Task<HttpResponseMessage> SkillThumbsProvider([FromBody] PaginatedRequest<ThumbsForSkillDetailRequest> request)
        {
            if (!(request.Data.TargetUser.IsMatch(x => request.Data.TargetUser, RegexPattern.UserName, ActionContext, ModelState)
             && Validation.StringLength(request.Data.TargetUser, x => request.Data.TargetUser, 6, 30, ActionContext, ModelState))
             && !request.Data.TargetUser.IsMatch(x => request.Data.TargetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetThumbsForSkillDetail(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to suggest skills.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-08", markType: 1, aliasName: "ProfilePersonal_SkillSuggestion")]
        [ActionName("SkillSuggestion"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/SkillSuggestion")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SkillSuggestion([FromBody] SkillSuggestionRequest request)
        {
            if (!(request.Suggestor.IsMatch(x => request.Suggestor, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(request.Suggestor, x => request.Suggestor, 6, 30, ActionContext, ModelState))
              && !request.Suggestor.IsMatch(x => request.Suggestor, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.UpsertSuggestSkill(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides the list of unapproved skill suggestions.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The list of unapproved skill suggestions.</returns>
        [HttpGet]
        [MetaData("2015-06-08", markType: 1, aliasName: "ProfilePersonal_UnApprovedSkillSuggestion")]
        [ActionName("UnApprovedSkillSuggestion"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/UnApprovedSkillSuggestion")]
        [ResponseType(typeof(IEnumerable<UnApprovedSkillSuggestionResponse>))]
        public async Task<HttpResponseMessage> GetUnApprovedSkillSuggestion(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
              && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetUnApprovedUserSkillSuggestion(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to accept or reject the suggested skill.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-09", markType: 1, aliasName: "ProfilePersonal_SkillAcceptance")]
        [ActionName("SkillAcceptance"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/SkillAcceptance")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SkillAcceptance([FromBody] SkillAcceptanceRequest request)
        {
            if (!(request.Suggestor.IsMatch(x => request.Suggestor, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(request.Suggestor, x => request.Suggestor, 6, 30, ActionContext, ModelState))
              && !request.Suggestor.IsMatch(x => request.Suggestor, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.AcceptSkill(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides employment history of the requested user.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The employment history of the user.</returns>
        [HttpGet]
        [MetaData("2015-06-12", markType: 1, aliasName: "ProfilePersonal_GetEmployment")]
        [ActionName("Employment"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Employment")]
        [ResponseType(typeof(IEnumerable<EmploymentWebResponse>))]
        public async Task<HttpResponseMessage> GetEmployment(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
              && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profilePersonalService.GetEmployment(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to add his/her employment history.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The employment ID of the employment.</returns>
        [HttpPut]
        [MetaData("2015-06-10", markType: 1, aliasName: "ProfilePersonal_AddEmployment")]
        [ActionName("Employment"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Employment")]
        [ResponseType(typeof(long))]
        public async Task<HttpResponseMessage> AddEmployment([FromBody] AddEmployeeRequest request)
        {
            var response = await _profilePersonalService.AddEmployment(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data });
        }

        /// <summary>
        /// Allows user to update his/her employment.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-10", markType: 1, aliasName: "ProfilePersonal_UpdateEmployment")]
        [ActionName("Employment"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Employment")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UpdateEmployment([FromBody] UpdateEmployeeRequest request)
        {
            var response = await _profilePersonalService.UpdateEmployment(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to delete his/her employment.
        /// </summary>
        /// <param name="request">The unique key representing the employment information of the user.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData("2015-06-10", markType: 1, aliasName: "ProfilePersonal_DeleteEmployment")]
        [ActionName("Employment"), Route(WebApiConfig.SystemRoutePrefix + "/Profile/Personal/Employment")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> DeleteEmployment([FromBody] SingleData<int> request)
        {
            var response = await _profilePersonalService.DeleteEmployment(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }
    }
}