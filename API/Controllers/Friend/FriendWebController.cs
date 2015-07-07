using Model.Attribute;
using Model.Base;
using Model.Common;
using Model.Friend;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Friend
{
    public partial class FriendController
    {
        /// <summary>
        /// Provides user with list of mutual friends against a given friend.
        /// </summary>
        /// <param name="request">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>Paginated list of mutual friends, along with total mutual friend's count.</returns>
        [HttpPost]
        [ActionName("Mutual")]
        [MetaData("2015-06-15", markType: 1, aliasName: "Friend_Mutual")]
        [ResponseType(typeof(PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>))]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<HttpResponseMessage> GetMutualFriends([FromBody]PaginatedRequest<string> request)
        {
            var response = await _service.GetMutualFriends(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Gets the list of user's web friends.
        /// </summary>
        /// <param name="request">The target user's unique identity number provided by the system or the unique user name chosen by the user.</param>
        /// <returns>Paginated list of friends.</returns>
        [HttpPost]
        [MetaData("2015-06-30", markType: 1, aliasName: "Friend_WebFriends")]
        [ActionName("WebFriends")]
        [ResponseType(typeof(PaginatedResponse<IEnumerable<WebFriendInformationResponse>>))]
        public async Task<HttpResponseMessage> GetWebFriends([FromBody]PaginatedRequest<string> request)
        {
            var response = await _service.GetWebFriends(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }
    }
}
