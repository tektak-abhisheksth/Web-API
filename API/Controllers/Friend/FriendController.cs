using BLL.Friend;
using Model.Attribute;
using Model.Friend;
using Model.Types;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Friend
{
    /// <summary>
    ///  Provides APIs to handle requests related to friends.
    /// </summary>
    [MetaData]
    public partial class FriendController : ApiController
    {
        private readonly IFriendService _service;

        /// <summary>
        /// Provides APIs to handle requests related to friends.
        /// </summary>
        /// <param name="service"></param>
        public FriendController(IFriendService service)
        {
            _service = service;
        }

        /// <summary>
        /// Allows to put new phone book contacts to server, delete existing phone book contacts from server, and sync server contacts in response.
        /// </summary>
        /// <param name="request">List of country codes and mobile numbers from the phone book, grouped to respective lists.</param>
        /// <returns>List of current user's friends.</returns>
        [ActionName("Sync")]
        [MetaData(markType: 3, aliasName: "Friend_Sync")]
        [ResponseType(typeof(FriendInformationCategorizedResponse))]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdatePhoneBookContacts([FromBody]PhoneBookContactsRequest request)
        {
            var updateResponse = await _service.UpdatePhoneBookContacts(request, Request.GetSession());
            if (updateResponse.Status.IsOperationSuccessful())
                return await Get(request.CTag);
            return Request.SystemResponse(updateResponse);
        }

        /// <summary>
        /// Gets the list of current user's friends, including pending ones.
        /// </summary>
        /// <param name="cTag">The contact tag that is stored in the device.</param>
        /// <returns>List of current user's friends.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Friend_Get")]
        [ResponseType(typeof(FriendInformationCategorizedResponse))]
        public async Task<HttpResponseMessage> Get(string cTag = null)
        {
            var deviceId = Request.GetUserInfo<string>(SystemSessionEntity.DeviceId);

            if (!Validation.StringLength(cTag, x => cTag, 36, 36, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.GetFriends(deviceId, cTag, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows to send friendship request.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Friend_Put")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Put([FromBody] FriendshipRequest request)
        {
            var response = await _service.RequestFriend(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows to accept or reject friendship request.
        /// </summary>
        /// <param name="request">The request information.</param>
        /// <returns>The status of the operation, along with system's CTag.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Friend_Post")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] FriendRespondRequest request)
        {
            var response = await _service.RespondFriendRequest(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data }, message: response.Message);
        }

        /// <summary>
        /// Allows to remove a friend.
        /// </summary>
        /// <param name="request">The request information.</param>
        /// <returns>The status of the operation, along with system's CTag.</returns>
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Friend_Delete")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete([FromBody] FriendRequest request)
        {
            var response = await _service.UnFriendRequest(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data }, message: response.Message);
        }

        /// <summary>
        /// Allows to cancel a friend request.
        /// </summary>
        /// <param name="request">The request information.</param>
        /// <returns>The status of the operation, along with system's CTag.</returns>
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Friend_CancelFriendRequest")]
        [ActionName("CancelFriendRequest")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> CancelFriendRequest([FromBody] FriendRequest request)
        {
            var response = await _service.UnFriendRequest(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data }, message: response.Message);
        }
    }
}
