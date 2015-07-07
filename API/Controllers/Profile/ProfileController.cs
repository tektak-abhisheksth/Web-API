using BLL.Profile;
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
    [MetaData]
    public partial class ProfileController : ApiController
    {
        private readonly IProfileService _profileService;

        /// <summary>
        /// Provides APIs to handle requests related to user profiles.
        /// </summary>
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Provides user with profile information of the requested target user.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>Detailed profile information of the user.</returns>
        [HttpGet]
        [MetaData("2015-02-25", markType: 3, aliasName: "Profile_Get")]
        [ResponseType(typeof(PersonalProfileResponse))]
        public async Task<HttpResponseMessage> Get(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
                   && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
                   && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            if (!Validation.Required(targetUser, x => targetUser, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.GetProfileInformation(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with profiles information of the requested target users.
        /// </summary>
        /// <param name="request">The target users' unique identity numbers provided by the system along with recorded timestamps.</param>
        /// <returns>Detailed profiles information of requested users whose timestamp is greater than provided timestamp.</returns>
        [HttpPost]
        [MetaData("2015-04-24", markType: 2, aliasName: "Profile_Profiles")]
        [ActionName("Profiles")]
        [ResponseType(typeof(List<ProfileOfflineResponse>))]
        public async Task<HttpResponseMessage> GetProfiles([FromBody]SingleData<List<GeneralKvPair<int, long>>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.GetProfiles(request.Data, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with basic information of the requested target user.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system, the unique user name chosen by the user, or the unique email address of the user.</param>
        /// <returns>Basic profile information of the user.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Profile_GetBasic")]
        [ActionName("Basic")]
        [ResponseType(typeof(BaseInfoResponse))]
        public async Task<HttpResponseMessage> Basic(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
                && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
                && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState)
                && !(targetUser.IsMatch(x => targetUser, RegexPattern.Email, ActionContext, ModelState)))
                return ActionContext.Response;

            if (!Validation.Required(targetUser, x => targetUser, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.GetBasicUserInformation(targetUser).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Updates user's current location.
        /// </summary>
        /// <param name="request">Current user's latitude, longitude and date time offset.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-02-04", markType: 3, aliasName: "Profile_Location")]
        [ActionName("Location")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Location(UserLocationRequest request)
        {
            var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);
            await _profileService.UpdateLocation(userId, request.Latitude, request.Longitude, request.Offset).ConfigureAwait(false);
            //HostingEnvironment.QueueBackgroundWorkItem(async x => await _profileService.UpdateLocation(userId, request.Latitude, request.Longitude, request.Offset));
            return Request.SystemResponse<string>(SystemDbStatus.Updated);
        }

        /// <summary>
        /// Allows current user to update profile picture.
        /// </summary>
        /// <param name="request">The file ID of the user.</param>
        /// <returns>The status of the operation.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [MetaData("2015-03-06", markType: 3, aliasName: "Profile_ProfilePicture")]
        [ActionName("ProfilePicture")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ProfilePicture([FromBody] SingleData<string> request)
        {
            var userName = Request.GetUserInfo<string>(SystemSessionEntity.UserName);

            var response = await _profileService.SaveProfilePicture(userName, request).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Allows current user to update existing basic information.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-03-14", markType: 3, aliasName: "Profile_ContactBasic")]
        [ActionName("ContactBasic")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ContactBasicUpdate([FromBody] BasicContactPersonRequest request)
        {
            var response = await _profileService.UpdateBasicContactPerson(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows current user to add a temporary contact to his/her/its profile.
        /// </summary>
        /// <param name="request">The temporary contact details.</param>
        /// <returns>The contact ID, list of contact chat network IDs and list of custom contact IDs.</returns>
        [HttpPut]
        [MetaData("2015-02-13", markType: 3, aliasName: "Profile_AddContact")]
        [ActionName("Contact")]
        [ResponseType(typeof(ContactResponse))]
        public async Task<HttpResponseMessage> ContactAddTemporary([FromBody] ContactRequest request)
        {
            var response = await _profileService.AddContact(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows current user to update existing basic contact details in his/her/its profile.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The contact ID, list of contact chat network IDs and list of custom contact IDs.</returns>
        [HttpPost]
        [MetaData("2015-02-13", markType: 3, aliasName: "Profile_UpdateContact")]
        [ActionName("Contact")]
        [ResponseType(typeof(ContactResponse))]
        public async Task<HttpResponseMessage> ContactUpdate([FromBody] UpdateContactRequest request)
        {
            var response = await _profileService.UpdateContact(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows current user to delete existing temporary contact from his/her/its profile.
        /// </summary>
        /// <param name="request">The contact ID of the contact to be deleted.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData("2015-02-13", markType: 3, aliasName: "Profile_DeleteContact")]
        [ActionName("Contact")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ContactDelete([FromBody] SingleData<long> request)
        {
            var response = await _profileService.DeleteContact(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows current user to delete existing chat network(s) from his/her/its contact for an existing contact.
        /// </summary>
        /// <param name="request">The contact ID along with the list of contact chat network IDs for the contact.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData("2015-02-13", markType: 3, aliasName: "Profile_ContactChatNetwork")]
        [ActionName("ContactChatNetwork")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ContactChatNetworksDelete([FromBody] SingleData<GeneralKvPair<long, List<int>>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data.Value, x => request.Data.Value, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.FlushContact(request, null, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows current user to delete existing custom contact field(s) from his/her/its contact for an existing contact.
        /// </summary>
        /// <param name="request">The contact ID along with the list of custom contact field IDs for the contact.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData("2015-02-13", markType: 3, aliasName: "Profile_ContactCustomField")]
        [ActionName("ContactCustomField")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ContactCustomFieldsDelete([FromBody] SingleData<GeneralKvPair<long, List<long>>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data.Value, x => request.Data.Value, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.FlushContact(null, request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with pending contact suggestions.
        /// </summary>
        /// <returns>Detailed suggested contact information of the user.</returns>
        // [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [MetaData("2015-03-11", markType: 3, aliasName: "Profile_GetContactSuggestions")]
        [ActionName("ContactSuggestion")]
        [ResponseType(typeof(ContactSuggestions))]
        public async Task<HttpResponseMessage> SuggestedContactGet()
        {
            var response = await _profileService.GetSuggestedContactList(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows a user to update his/her/its previous suggestion made to other user's contact, that can be either for simple major fields, for contact chat fields, or for custom fields.
        /// </summary>
        /// <param name="request">Simple fields with new values, list of contact chat networks with new values and their custom contact field IDs (send when updating or send null if adding), and list of custom fields with name-value pair and their custom contact field IDs (send when updating or send null if adding).</param>
        /// <returns>The contact ID, list of contact chat network IDs and list of custom contact IDs.</returns>
        [HttpPost]
        [MetaData("2015-03-11", markType: 3, aliasName: "Profile_UpdateContactSuggestion")]
        [ActionName("ContactSuggestion")]
        [ResponseType(typeof(ContactResponse))]
        public async Task<HttpResponseMessage> SuggestedContactUpdate([FromBody] UpdateSuggestContactRequest request)
        {
            var response = await _profileService.UpdateSuggestedContact(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows a user to delete his/her/its previous suggestion made to other user's contact, that can be either for simple major fields, for contact chat fields, or for custom fields.
        /// </summary>
        /// <param name="request">The contact ID and list of custom contact IDs.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData("2015-03-11", markType: 3, aliasName: "Profile_DeleteContactSuggestions")]
        [ActionName("ContactSuggestion")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SuggestedContactDelete([FromBody] SingleData<GeneralKvPair<long, List<long>>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data.Value, x => request.Data.Value, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.DeleteSuggestedContact(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows a user to approve or reject the suggested contact request.
        /// </summary>
        /// <param name="request">The custom ID and mode that represents "1" for "Approval" and "0" for "Rejection".</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-03-12", markType: 3, aliasName: "Profile_ContactSuggestionResponse")]
        [ActionName("ContactSuggestionResponse")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SuggestedContactOperation([FromBody] SuggestedContactOperationRequest request)
        {
            var response = await _profileService.SuggestedContactOperation(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Provides user with the list of contact settings.
        /// </summary>
        /// <param name="contactId">The unique contact ID for the contact.</param>
        /// <returns>Contact setting list.</returns>
        [HttpGet]
        [MetaData("2015-03-02", markType: 3, aliasName: "Profile_GetContactSettings")]
        [ActionName("ContactSetting")]
        [ResponseType(typeof(IEnumerable<ContactSettingResponse>))]
        public async Task<HttpResponseMessage> GetContactSetting(int contactId)
        {
            var response = await _profileService.GetContactSettings(contactId, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with the list of contact settings categories.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>Contact settings categories list.</returns>
        [HttpPost]
        [MetaData("2015-04-02", markType: 3, aliasName: "Profile_GetContactSettingCategories")]
        [ActionName("ContactSettingCategory")]
        [ResponseType(typeof(IEnumerable<ContactSettingCategoriesResponse>))]
        public async Task<HttpResponseMessage> GetContactSettingCategory([FromBody] ContactSettingCategoriesRequest request)
        {
            var response = await _profileService.GetContactSettingCategory(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with the list of contact settings friends.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>Contact settings friends list.</returns>
        [HttpPost]
        [MetaData("2015-04-02", markType: 3, aliasName: "Profile_GetContactSettingFriends")]
        [ActionName("ContactSettingFriend")]
        [ResponseType(typeof(IEnumerable<ContactSettingFriendsResponse>))]
        public async Task<HttpResponseMessage> GetContactSettingFriend([FromBody] ContactSettingCategoriesRequest request)
        {
            var response = await _profileService.GetContactSettingFriend(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows current user to update existing contact settings.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-04-03", markType: 3, aliasName: "Profile_UpdateContactSetting")]
        [ActionName("ContactSetting")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ContactSettingUpdate([FromBody] ContactSettingRequest request)
        {
            var response = await _profileService.UpsertContactSetting(request, request.Flush ? (byte)SystemDbStatus.Flushed : (byte)SystemDbStatus.Updated, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user the details of requested user's availability status.
        /// </summary>
        /// <param name="targetUser">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>The details of availability status.</returns>
        [HttpGet]
        [MetaData("2015-06-17", markType: 3, aliasName: "Profile_GetAvailability")]
        [ActionName("Availability")]
        [ResponseType(typeof(UserStatusResponse))]
        public async Task<HttpResponseMessage> UserAvailabilityGet(string targetUser)
        {
            if (!(targetUser.IsMatch(x => targetUser, RegexPattern.UserName, ActionContext, ModelState)
              && Validation.StringLength(targetUser, x => targetUser, 6, 30, ActionContext, ModelState))
              && !targetUser.IsMatch(x => targetUser, RegexPattern.Numeric, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _profileService.GetUserAvailability(targetUser, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to update the user's availability status.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-06-18", markType: 3, aliasName: "Profile_PostAvailability")]
        [ActionName("Availability")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UserAvailabilityUpdate([FromBody] StatusSetRequest request)
        {
            var response = await _profileService.UpdateUserAvailability(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts data from user to update the information in who viewed your profile section.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData("2015-06-30", markType: 3, aliasName: "Profile_SignalView")]
        [ActionName("SignalView")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SignalView([FromBody] SignalViewRequest request)
        {
            var response = await _profileService.SignalView(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        #region Old codes
        ///// <summary>
        ///// Allows current user to add chat network(s) in his/her/its contact for an existing contact.
        ///// </summary>
        ///// <param name="request">A list of new chat network details.</param>
        ///// <returns>List of contact chat network IDs.</returns>
        //[HttpPut]
        //[MetaData("2015-02-13")]
        //[ActionName("ContactChatNetwork")]
        //[ResponseType(typeof(List<int>))]
        //public async Task<HttpResponseMessage> ContactChatNetworksAdd([FromBody] MetaChatNetworkRequest request)
        //{
        //    var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);

        //    if (!Validation.IsEnumerablePopulated(request.ChatNetworks, x => request.ChatNetworks, ActionContext, ModelState))
        //        return ActionContext.Response;

        //    var response = await _profileService.AddMeta(userId, request, null).ConfigureAwait(false);
        //    return Request.SystemResponse(response.Status, response.Data.ContactChatNetworks);
        //}

        ///// <summary>
        ///// Allows current user to update existing chat network(s) in his/her/its contact for an existing contact.
        ///// </summary>
        ///// <param name="request">A list of existing chat network details.</param>
        ///// <returns>The status of the operation.</returns>
        //[HttpPost]
        //[MetaData("2015-02-13")]
        //[ActionName("ContactChatNetwork")]
        //[ResponseType(typeof(string))]
        //public async Task<HttpResponseMessage> ContactChatNetworksUpdate([FromBody] UpdateUserChatNetworkRequest request)
        //{
        //    var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);

        //    if (!Validation.IsEnumerablePopulated(request.ChatNetworks, x => request.ChatNetworks, ActionContext, ModelState))
        //        return ActionContext.Response;

        //    var response = await _profileService.UpdateMeta(userId, request, null).ConfigureAwait(false);
        //    return Request.SystemResponse(response);
        //}

        ///// <summary>
        ///// Allows current user to add custom contact field(s) in his/her/its contact for an existing contact.
        ///// </summary>
        ///// <param name="request">The request body.</param>
        ///// <returns>List of custom contact IDs.</returns>
        //[HttpPut]
        //[MetaData("2015-02-13")]
        //[ActionName("ContactCustomField")]
        //[ResponseType(typeof(List<long>))]
        //public async Task<HttpResponseMessage> ContactCustomFieldsAdd([FromBody] MetaCustomContactNetwork request)
        //{
        //    var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);

        //    if (!Validation.IsEnumerablePopulated(request.CustomContacts, x => request.CustomContacts, ActionContext, ModelState))
        //        return ActionContext.Response;

        //    var response = await _profileService.AddMeta(userId, null, request).ConfigureAwait(false);
        //    return Request.SystemResponse(response.Status, response.Data.ContactCustoms);
        //}

        ///// <summary>
        ///// Allows current user to update existing custom contact field(s) in his/her/its contact for an existing contact.
        ///// </summary>
        ///// <param name="request">The request body.</param>
        ///// <returns>The status of the operation.</returns>
        //[HttpPost]
        //[MetaData("2015-02-13")]
        //[ActionName("ContactCustomField")]
        //[ResponseType(typeof(string))]
        //public async Task<HttpResponseMessage> ContactCustomFieldsUpdate([FromBody] UpdateUserCustomContactRequest request)
        //{
        //    var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);

        //    if (!Validation.IsEnumerablePopulated(request.CustomContacts, x => request.CustomContacts, ActionContext, ModelState))
        //        return ActionContext.Response;

        //    var response = await _profileService.UpdateMeta(userId, null, request).ConfigureAwait(false);
        //    return Request.SystemResponse(response);
        //}

        ///// <summary>
        ///// Allows a user to add a suggestion made to other user's contact, that can be either for simple major fields, for contact chat fields, or for custom fields.
        ///// </summary>
        ///// <param name="request">Simple fields with new values (if any), list of contact chat networks with new values, and list of custom fields with name-value pair.</param>
        ///// <returns>List of (custom contact) IDs for all suggestions with 0 as a place holder for those that were left unchanged. NB: Any added simple field, chat network field, or custom field will be treated as custom contact fields until approved, thus the returned IDs uniquely represent each suggestions made, and in a strict order.</returns>
        //[HttpPut]
        //[MetaData("2015-03-11")]
        //[ActionName("ContactSuggestion")]
        //[ResponseType(typeof(IEnumerable<long>))]
        //public async Task<HttpResponseMessage> SuggestedContactAdd([FromBody] AddSuggestContactRequest request)
        //{
        //    var response = await _profileService.AddSuggestedContact(request).ConfigureAwait(false);
        //    return Request.SystemResponse(response);
        //}
        #endregion
    }
}
