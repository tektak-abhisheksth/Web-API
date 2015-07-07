using BLL.Category;
using Model.Attribute;
using Model.Category;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Category
{
    /// <summary>
    /// Provides APIs to handle requests related to user categories.
    /// </summary>
    [MetaData]
    public partial class CategoryController : ApiController
    {
        private readonly ICategoryService _service;

        /// <summary>
        /// Provides APIs to handle requests related to user categories.
        /// </summary>
        /// <param name="service"></param>
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the list of user's categories.
        /// </summary>
        /// <returns>List of user's categories.</returns>
        [MetaData(markType: 3, aliasName: "Category_Get")]
        [ResponseType(typeof(IEnumerable<CategoryResponse>))]
        public async Task<HttpResponseMessage> Get()
        {
            // var response = await _service.GetUserCategoryList(userId, userName, token, deviceId);
            var response = await _service.GetUserCategoryList(Request.GetSession()).ConfigureAwait(false);

            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows to add new category.
        /// </summary>
        /// <param name="request">Information about the new category.</param>
        /// <returns>System assigned category id.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Category_Put")]
        [ResponseType(typeof(byte?))]
        public async Task<HttpResponseMessage> Put([FromBody] CategoryAddRequest request)
        {
            var response = await _service.InsertCategory(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data }, message: response.Message);
        }

        /// <summary>
        /// Allows to edit existing category.
        /// </summary>
        /// <param name="request">Updated information about the existing category.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Category_Post")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] CategoryUpdateRequest request)
        {
            var response = await _service.UpdateCategory(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows to remove existing category.
        /// </summary>
        /// <param name="request">The system-provided list of IDs of the category.</param>
        /// <returns>The status of the operation.</returns>
        //   [ActionName("Delete")]
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Category_Delete")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete([FromBody] DeleteCategory request)
        {
            var response = await _service.DeleteCategory(request, SystemDbStatus.Deleted, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows to add friends in existing category.
        /// </summary>
        /// <param name="request">List of system provided user IDs and category ID of existing friends.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Category_AddFriends")]
        [ActionName("CategoryFriends")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> AddFriendsInCategory([FromBody] CategoryFriends request)
        {
            var response = await _service.UpsertCategoryFriends(request, SystemDbStatus.Inserted, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows to delete existing friends from the respective category.
        /// </summary>
        /// <param name="request">List of system provided user IDs and category ID of existing friends.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Category_DeleteFriends")]
        [ActionName("CategoryFriends")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> RemoveFriendsInCategory([FromBody] CategoryFriends request)
        {
            var response = await _service.UpsertCategoryFriends(request, SystemDbStatus.Deleted, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        ///// <summary>
        ///// Allows to copy friends from one category to one or more of other existing categories.
        ///// </summary>
        ///// <param name="request">Information about the source and destination categories along with friends' list to be copied.</param>
        ///// <returns>The status of the operation.</returns>
        //[HttpPut]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public HttpResponseMessage CopyCategory([FromBody] CopyCategory request)
        //{
        //    var response = this.service.CopyCategory(request, SystemDatabaseOperationMode.Copy);
        //    return this.Request.SystemResponse<string>(response);
        //}

        ///// <summary>
        ///// Allows to move friends from one category to one or more of other existing categories.
        ///// </summary>
        ///// <param name="request">Information about the source and destination categories along with friends' list to be moved.</param>
        ///// <returns>The status of the operation.</returns>
        //[HttpPost]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[ActionName("MoveFriends")]
        //public HttpResponseMessage MoveCategory([FromBody] CopyCategory request)
        //{
        //    var response = this.service.CopyCategory(request, SystemDatabaseOperationMode.Move);
        //    return this.Request.SystemResponse<string>(response);

        //}

        ///// <summary>
        ///// Gets the list of users in the respective category.
        ///// </summary>
        ///// <param name="categoryId">The user category id assigned to the entity by the system.</param>
        ///// <param name="pageIndex">Optional. The index of the page.</param>
        ///// <param name="pageSize">Optional. The size of the page</param>
        ///// <returns>List of users in the respective category.</returns>
        //[HttpGet]
        //[ActionName("FriendsInCategory")]
        //public async Task<HttpResponseMessage> GetFriendInCategory(int categoryId, int? pageIndex = null, int? pageSize = null)
        //{
        //    var userId = this.Request.GetUserInfo<int>(SystemSessionEntity.UserId);
        //    var response = await this.service.GetFriendsInCategoryList(userId, categoryId, pageIndex, pageSize);
        //    return this.Request.SystemResponse(SystemDbStatus.Selected, response);
        //}
    }
}
