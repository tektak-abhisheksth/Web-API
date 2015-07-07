using Model.Attribute;
using Model.Category;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Category
{
    public partial class CategoryController
    {
        /// <summary>
        /// Accepts list of friends from user to move or copy them to one or more target categories.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData("2015-07-01", markType: 1, aliasName: "Category_CategoryFriendsCopy")]
        [ActionName("CategoryFriendsCopy")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> CategoryFriendsCopy([FromBody] CopyCategory request)
        {
            var response = await _service.CategoryFriendsCopy(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts data from user to enlist the friends of respective category.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The list of friends in category.</returns>
        [HttpPost]
        [MetaData("2015-07-02", markType: 1, aliasName: "Category_FriendsInCategory")]
        [ActionName("CategoryFriends")]
        [ResponseType(typeof(PaginatedResponse<IEnumerable<FriendsInCategoryResponse>>))]
        public async Task<HttpResponseMessage> CategoryFriends([FromBody] PaginatedRequest<FriendsInCategoryRequest> request)
        {
            var response = await _service.GetFriendsInCategory(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }
    }
}
