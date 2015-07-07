using BLL.Inbox;
using Model.Attribute;
using Model.Common;
using Model.Inbox;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Inbox
{
    /// <summary>
    /// Provides APIs to handle requests related to inbox, rules and messages.
    /// </summary>
    [MetaData]
    public partial class InboxController : ApiController
    {
        private readonly IInboxService _service;

        /// <summary>
        /// Provides APIs to handle requests related to inbox, rules and messages.
        /// </summary>
        /// <param name="service"></param>
        public InboxController(IInboxService service)
        {
            _service = service;
        }

        /// <summary>
        /// Provides user with list of inbox folders.
        /// </summary>
        /// <returns>Detailed inbox folders' information.</returns>
        [MetaData(markType: 3, aliasName: "Inbox_Get")]
        [ResponseType(typeof(IEnumerable<InboxResponse>))]
        public async Task<HttpResponseMessage> Get()
        {
            var response = await _service.GetInboxFolder(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with list of inbox folders' rules.
        /// </summary>
        /// <param name="folderId">System provided folder ID.</param>
        /// <returns>Detailed inbox folders' rules' information.</returns>
        [MetaData(markType: 3, aliasName: "Inbox_GetRules")]
        [ActionName("Rules")]
        [ResponseType(typeof(IEnumerable<RuleResponse>))]
        public async Task<HttpResponseMessage> GetRules(int? folderId = null)
        {
            var response = await _service.GetInboxFolderRules(folderId, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts data from the user to create the inbox and corresponding rules for the respective inbox.
        /// </summary>
        /// <param name="request">Folder name and optionally list of rules.</param>
        /// <returns>System provided folder ID and message rule IDs.</returns>
        [MetaData(markType: 3, aliasName: "Inbox_Put")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Put([FromBody] InboxRequest request)
        {
            int? folderId = 0;

            var response = await _service.UpsertInbox(request, folderId, SystemDbStatus.Inserted, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { FolderId = response.Data.Id, MessageRuleList = response.Data.Value }, message: response.Message);
        }

        /// <summary>
        /// Accepts data from the user to update the inbox name.
        /// </summary>
        /// <param name="request">Folder ID and folder name to be updated.</param>
        /// <returns>The status of the operation.</returns>
        [MetaData(markType: 3, aliasName: "Inbox_Post")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] SingleData<GeneralKvPair<int, string>> request)
        {
            if (!Validation.Required(request.Data.Value, x => request.Data.Value, ActionContext, ModelState))
                return ActionContext.Response;

            var requestCasted = new InboxRequest { Name = request.Data.Value, UserId = request.UserId, DeviceId = request.DeviceId };
            var response = await _service.UpsertInbox(requestCasted, request.Data.Id, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status, message: response.Message);
        }

        /// <summary>
        /// Accepts data from the user to delete the inbox.
        /// </summary>
        /// <param name="request">Folder IDs to be deleted.</param>
        /// <returns>The status of the operation.</returns>
        [MetaData(markType: 3, aliasName: "Inbox_Delete")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete([FromBody] SingleData<List<int>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.DeleteInbox(request.Data, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts data from user to mute/unmute specified inbox.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Inbox_Mute")]
        [ActionName("Mute")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> MuteInbox([FromBody] InboxMuteRequest request)
        {
            if (!Validation.IsEnumerablePopulated(request.FolderList, x => request.FolderList, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.MuteInbox(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts data from the user to create a rule for given inbox folder.
        /// </summary>
        /// <param name="request">Folder ID of an existing inbox.</param>
        /// <returns>Provided folder ID and new message rule IDs.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Inbox_AddRule")]
        [ActionName("Rules")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> AddRule([FromBody] RuleAddRequest request)
        {
            var response = await _service.UpsertRule(request, SystemDbStatus.Inserted, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { FolderId = response.Data.Id, MessageRuleList = response.Data.Value }, message: response.Message);
        }

        /// <summary>
        /// Accepts data from the user to update existing rule.
        /// </summary> 
        /// <param name="request">The rule information to be updated.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Inbox_UpdateRule")]
        [ActionName("Rules")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UpdateRule([FromBody] RuleUpdateRequest request)
        {
            var response = await _service.UpsertRule(request, SystemDbStatus.Updated, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts message rule ID as ID and folder ID as Value from the user to delete existing rule.
        /// </summary>
        /// <param name="request">The message rule ID and message folder ID to be deleted.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Inbox_DeleteRule")]
        [ActionName("Rules")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> DeleteRule([FromBody]SingleData<GeneralKvPair<long, int>> request)
        {
            var response = await _service.DeleteRule(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status, null, message: response.Message);
        }
    }
}
