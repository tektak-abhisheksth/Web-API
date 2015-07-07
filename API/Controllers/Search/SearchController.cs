using BLL.Search;
using Model.Attribute;
using Model.Common;
using Model.Search;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Search
{
    /// <summary>
    /// Provides APIs to handle requests related to user search.
    /// </summary>
    [MetaData]
    public partial class SearchController : ApiController
    {
        private readonly ISearchService _service;

        /// <summary>
        /// Provides APIs to handle requests related to user search.
        /// </summary>
        /// <param name="service"></param>
        public SearchController(ISearchService service)
        {
            _service = service;
        }


        /// <summary>
        /// Gets the list of available iLoop users based on the provided search string.
        /// </summary>
        /// <param name="searchTerm">The string to be searched.</param>
        /// <param name="userTypeId">The user type ID of the iLoop user provided by the system.</param>
        /// <param name="isConnected">The friendship status.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>Search results (list of users in the system) as per the query string.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Search_BasicSearch")]
        [ActionName("BasicSearch")]
        [ResponseType(typeof(PaginatedResponse<IEnumerable<BasicSearchResponse>>))]
        public async Task<HttpResponseMessage> BasicSearch(string searchTerm, byte? userTypeId = null, byte? isConnected = null, int pageIndex = 0, int pageSize = 25)
        {
            if (!Validation.StringLength(searchTerm, x => searchTerm, 1, 40, ActionContext, ModelState))
                return ActionContext.Response;

            var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);
            var deviceId = Request.GetUserInfo<string>(SystemSessionEntity.DeviceId);

            var response = await _service.BasicSearch(userId, deviceId, searchTerm, userTypeId, isConnected, pageIndex, pageSize).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Gets the list of available iLoop users based on the provided search string.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>Search results (list of users in the system) as per the query string</returns>
        [HttpPost]
        [MetaData("2015-06-23", markType: 1, aliasName: "Search_Search")]
        [ActionName("Search")]
        [ResponseType(typeof(UserSearchResponse))]
        public async Task<HttpResponseMessage> Search(UserSearchRequest request)
        {
            var response = new StatusData<UserSearchResponse> { Status = SystemDbStatus.Selected, Data = await _service.UserSearch(request, Request.GetSession()).ConfigureAwait(false) };
            return Request.SystemResponse(response);
        }
    }
}
