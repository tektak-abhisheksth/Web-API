using API.Filters;
using Model.Attribute;
using Model.Common;
using Model.Profile;
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
    /// <summary>
    /// Provides APIs to handle requests related to user profiles.
    /// </summary>
    // [ApiExplorerSettings(IgnoreApi = true)]
    public partial class ProfileController
    {
        /// <summary>
        /// Provides user with list of basic information of provided multiple target users.
        /// </summary>
        /// <param name="request">The list of target users' unique identity numbers provided by the system, or the unique user names chosen by the users.</param>
        /// <returns>List of basic profile information of the users.</returns>
        [HttpPut]
        [MetaData("2015-06-12", aliasName: "ProfilePersonal_PutBasic")]
        [ActionName("Basic")]
        [ResponseType(typeof(IEnumerable<BaseInfoResponse>))]
        public async Task<HttpResponseMessage> BasicUsers(SingleData<List<string>> request)
        {
            var response = new StatusData<IEnumerable<BaseInfoResponse>> { Status = SystemDbStatus.Unauthorized };
            for (var index = 0; index < request.Data.Count; ++index)
            {
                var user = request.Data[index];
                if (!(user.IsMatch(x => user, RegexPattern.UserName, ActionContext, ModelState)
                      && Validation.StringLength(user, x => user, 6, 30, ActionContext, ModelState))
                    && !user.IsMatch(x => user, RegexPattern.Numeric, ActionContext, ModelState)
                    && !(user.IsMatch(x => user, RegexPattern.Email, ActionContext, ModelState)))
                    return ActionContext.Response;

                if (!Validation.Required(user, x => user, ActionContext, ModelState))
                    return ActionContext.Response;
            }

            response.Data = await _profileService.GetBasicUsersInformation(request, Request.GetSession()).ConfigureAwait(false);
            response.Status = SystemDbStatus.Selected;

            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with basic information of the requested target user, for cases when no user is logged in (for example, forgot password).
        /// </summary>
        /// <param name="request">The target user's unique identity number provided by the system, the unique user name chosen by the user, or the unique email address of the user.</param>
        /// <returns>Basic profile information of the user.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-06-04", markType: 1, aliasName: "ProfilePersonal_PostBasic")]
        [ActionName("Basic")]
        [ResponseType(typeof(BaseInfoResponse))]
        public async Task<HttpResponseMessage> Basic(SingleDataAnonymous<string> request)
        {
            if (!(request.Data.IsMatch(x => request.Data, RegexPattern.UserName, ActionContext, ModelState)
                  && Validation.StringLength(request.Data, x => request.Data, 6, 30, ActionContext, ModelState))
                && !request.Data.IsMatch(x => request.Data, RegexPattern.Numeric, ActionContext, ModelState)
                && !(request.Data.IsMatch(x => request.Data, RegexPattern.Email, ActionContext, ModelState)))
                return ActionContext.Response;

            var response = new StatusData<BaseInfoResponse>
             {
                 Status = SystemDbStatus.Selected,
                 Data = await _profileService.GetBasicUserInformation(request.Data).ConfigureAwait(false)
             };

            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with his/her biography.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The biography of the user.</returns>
        [HttpGet]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_GetAbout")]
        [ActionName("About")]
        [ResponseType(typeof(AboutResponse))]
        public async Task<HttpResponseMessage> GetAbout(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
               && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
               && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.GetUserAbout(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to update his/her biography.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-03", markType: 1, aliasName: "ProfilePersonal_PostAbout")]
        [ActionName("About")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UpdateAbout([FromBody] AboutRequest request)
        {
            var response = await _profileService.UpdateUserAbout(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with profile view details.
        /// </summary>
        /// <param name="request">The profile view type and the position/skill type id (if applicable).</param>
        /// <returns>List of profile visitors. Information contains total views.</returns>
        [HttpPost]
        [MetaData("2015-06-12", markType: 1, aliasName: "ProfilePersonal_ViewDetail")]
        [ActionName("ViewDetail")]
        [ResponseType(typeof(PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>))]
        public async Task<HttpResponseMessage> GetViewDetail([FromBody] PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>> request)
        {
            var response = await _profileService.GetProfileViewDetail(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with profile view panel information.
        /// </summary>
        /// <returns>List of profile visitors. Information contains total distinct types of positions/skills available.</returns>
        [HttpGet]
        [MetaData("2015-06-12", markType: 1, aliasName: "ProfilePersonal_ViewPanel")]
        [ActionName("ViewPanel")]
        // [ApiExplorerSettings(IgnoreApi = true)]
        [ResponseType(typeof(InformationResponse<IEnumerable<ViewerPanelResponse>, int>))]
        public async Task<HttpResponseMessage> GetViewPanel()
        {
            var response = await _profileService.GetProfileViewPanel(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with profile view summary.
        /// </summary>
        /// <param name="request">The view type.</param>
        /// <returns>List of profile view types. Information contains total view counts.</returns>
        [HttpPost]
        [MetaData("2015-06-12", markType: 1, aliasName: "ProfilePersonal_ViewSummary")]
        [ActionName("ViewSummary")]
        [ResponseType(typeof(InformationResponse<IEnumerable<ViewerPanelResponse>, int>))]
        public async Task<HttpResponseMessage> GetViewSummary([FromBody] PaginatedRequest<int> request)
        {
            var response = await _profileService.GetProfileViewSummary(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }
    }
}
