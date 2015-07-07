using BLL.Chat;
using Model.Attribute;
using Model.Chat;
using Model.Common;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Chat
{
    /// <summary>
    /// Provides APIs to handle requests related to chat.
    /// </summary>
    [MetaData]
    public partial class ChatController : ApiController
    {
        private readonly IChatService _service;

        /// <summary>
        /// Provides APIs to handle requests related to inbox, rules and messages.
        /// </summary>
        /// <param name="service"></param>
        public ChatController(IChatService service)
        {
            _service = service;
        }

        /// <summary>
        /// Transfers the list of messages of a particular instance.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>List of messages.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_Message")]
        [ActionName("Message")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> MessagePull([FromBody] ChatRequest.MessageRequest request)
        {
            var response = await _service.MessagePull(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, response.Data);
        }

        /// <summary>
        /// Transfers the message information of the provided instance ID and message ID of a particular user.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The message information.</returns>
        [HttpPost]
        [MetaData("2015-02-10", markType: 3, aliasName: "Chat_MessageInformation")]
        [ActionName("MessageInformation")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] ChatRequest.MessageInformationRequest request)
        {
            var response = await _service.GetMessage(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Transfers the list of messages of a particular instance asynchronously.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>List of messages.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_TinyMessagePush")]
        [ActionName("TinyMessagePush")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> TinyMessagePush([FromBody] ChatRequest.MessageRequest request)
        {
            var response = await _service.TinyMessagePush(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts data from user to delete specified message(s).
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>List of deleted message.</returns>
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Chat_Delete")]
        [ActionName("Message")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete([FromBody] ChatRequest.MessageDeleteRequest request)
        {
            if (!Validation.IsEnumerablePopulated(request.MessageList, x => request.MessageList, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.MessageDelete(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Deleted, response);
        }

        /// <summary>
        /// Transfers the list of instances of a particular user.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>List of instances.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_Instance")]
        [ActionName("Instance")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] ChatRequest.InstanceRequest request)
        {
            var response = await _service.InstancePull(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, response.Data);
        }

        /// <summary>
        /// Transfers the instance information of the provided instance ID of a particular user.
        /// </summary>
        /// <param name="request">The instance ID.</param>
        /// <returns>The instance information.</returns>
        [HttpPost]
        [MetaData("2015-02-10", markType: 3, aliasName: "Chat_InstanceInformation")]
        [ActionName("InstanceInformation")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] SingleData<string> request)
        {

            if (!Validation.Required(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.GetInstance(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }


        /// <summary>
        /// Transfers the list of instances of a particular user asynchronously.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>List of instances.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_TinyInstancePush")]
        [ActionName("TinyInstancePush")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> TinyInstancePush([FromBody] ChatRequest.InstanceRequest request)
        {
            var response = await _service.TinyInstancePush(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts data from user to leave/delete/leave and delete specified instances.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_InstanceOperation")]
        [ActionName("InstanceOperation")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] ChatRequest.InstanceOperationRequest request)
        {
            if (!Validation.IsEnumerablePopulated(request.InstanceList, x => request.InstanceList, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.InstanceOperation(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, response.Data);
        }

        ///// <summary>
        ///// Accepts data from user to insert group GUID for associated group members.
        ///// </summary>
        ///// <param name="request">The request body.</param>
        ///// <returns>The status of the operation.</returns>
        //[HttpPut]
        //[ActionName("Group")]
        //public async Task<HttpResponseMessage> PutGroup([FromBody] GroupUserRequest request)
        //{
        //    if (!Validation.IsEnumerablePopulated(request.Users, x => request.Users, ActionContext, ModelState))
        //        return ActionContext.Response;

        //    var response = await _service.SaveGroup(request, Request.GetSession());
        //    return Request.SystemResponse(response.Status, response.Data);
        //}

        /// <summary>
        ///  Provides user with list of associated group GUIDs.
        /// </summary>
        /// <returns>List of group GUIDs.</returns>
        //[HttpGet]
        //[ActionName("Group")]
        //public async Task<HttpResponseMessage> GetGroup()
        //{
        //    var response = await _service.GetGroups(Request.GetSession());
        //    return Request.SystemResponse(SystemDbStatus.Selected, response);
        //}

        /// <summary>
        /// Accepts data from user to block/unblock specified instances.
        /// </summary>
        /// <param name="request">The list of instance IDs.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_BlockUnblockInstances")]
        [ActionName("BlockUnblockInstances")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> BlockUnBlockInstances([FromBody] SingleData<List<string>> request)
        {

            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.BlockUnBlockInstances(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Accepts data from user to mute/unmute specified instances.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_MuteUnmuteInstances")]
        [ActionName("MuteUnmuteInstances")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> MuteUnMuteInstances([FromBody] SingleData<List<string>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.MuteUnMuteInstances(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Accepts data from user to insert list of users to block/unblock in specified instance ID.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_BlockUsers")]
        [ActionName("BlockUsers")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> BlockUsers([FromBody] ChatRequest.BlockUsersRequest request)
        {
            if (!Validation.IsEnumerablePopulated(request.Users, x => request.Users, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.BlockUsers(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Accepts data from user to insert list of users to mute/unmute.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_MuteUsers")]
        [ActionName("MuteUsers")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> MuteUsers([FromBody] SingleData<List<string>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.MuteUsers(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Provides user with list of blocked instances.
        /// </summary>
        /// <returns>List of blocked instances.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_BlockedInstances")]
        [ActionName("BlockedInstances")]
        [ResponseType(typeof(IEnumerable<ChatResponse.GroupInstanceResponse>))]
        public async Task<HttpResponseMessage> BlockedInstances()
        {
            var response = await _service.GetBlockedInstances(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with the list of blocked users in the instances.
        /// </summary>
        /// <returns>List of blocked users in the instances.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_UserBlockedInstances")]
        [ActionName("UserBlockedInstances")]
        [ResponseType(typeof(IEnumerable<ChatResponse.UserBlockedInstancesResponse>))]
        public async Task<HttpResponseMessage> UserBlockedInstances()
        {
            var response = await _service.GetUserBlockedInstances(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with the list of mute instances.
        /// </summary>
        /// <returns>List of mute instances.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_MutedInstances")]
        [ActionName("MutedInstances")]
        [ResponseType(typeof(IEnumerable<ChatResponse.GroupInstanceResponse>))]
        public async Task<HttpResponseMessage> MutedInstances()
        {
            var response = await _service.GetMutedInstances(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with the list of muted users.
        /// </summary>
        /// <returns>The list of muted users.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_MutedUsers")]
        [ActionName("MutedUsers")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ResponseType(typeof(IEnumerable<ChatResponse.ChatUserBlockedResponse>))]
        public async Task<HttpResponseMessage> MutedUsers()
        {
            var response = await _service.GetMutedUsers(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts data from user to add/remove chat members.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_AddRemoveMembers")]
        [ActionName("AddRemoveMembers")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody]ChatRequest.BlockUsersRequest request)
        {
            if (!Validation.IsEnumerablePopulated(request.Users, x => request.Users, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.ChatMemberAddRemove(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to add new chat group.
        /// </summary>
        /// <param name="request">Information about the new chat group.</param>
        /// <returns>The system assigned group ID.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Chat_Group")]
        [ActionName("Group")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SaveGroup([FromBody] ChatRequest.ChatGroupRequest request)
        {

            var response = await _service.GroupAdd(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data });
        }

        /// <summary>
        /// Provides user with the list of group members.
        /// </summary>
        /// <param name="groupId">System provided group ID.</param>
        /// <returns>The list of group members.</returns>
        [HttpGet]
        [ActionName("Group")]
        [MetaData(markType: 3, aliasName: "Chat_GetGroupMember")]
        [ResponseType(typeof(IEnumerable<string>))]
        public async Task<HttpResponseMessage> GetGroupMember(string groupId)
        {
            var response = await _service.GetGroupMember(groupId, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, response.Data);
        }

        /// <summary>
        /// Allows user to leave specified group.
        /// </summary>
        /// <param name="request">System provided group ID.</param>
        /// <returns>The list of leaved group IDs.</returns>
        [HttpPost]
        [MetaData("2015-02-12", markType: 3, aliasName: "Chat_LeaveGroup")]
        [ActionName("LeaveGroup")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> LeaveGroup([FromBody] SingleData<List<string>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.LeaveGroup(request.Data, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts data from the user to update the group name.
        /// </summary>
        /// <param name="request">group ID and group name to be updated.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_PostGroup")]
        [ActionName("Group")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] SingleData<GeneralKvPair<string, string>> request)
        {
            if (!Validation.Required(request.Data.Id, x => request.Data.Id, ActionContext, ModelState))
                return ActionContext.Response;

            var requestCasted = new ChatRequest.ChatGroupRequest { GroupName = request.Data.Value, UserId = request.UserId, DeviceId = request.DeviceId };
            var response = await _service.UpdateGroupName(requestCasted, request.Data.Id, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Allows user to add setting to the specified group. 
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Chat_GroupSetting")]
        [ActionName("GroupSetting")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SaveGroupSetting([FromBody] ChatRequest.GroupSettingRequest request)
        {
            var response = await _service.AddGroupSetting(request, Request.GetSession()).ConfigureAwait(false);
            //return Request.SystemResponse(response.Status, new { GroupId = response.Data });
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Allows user to add members in the specified group.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Chat_GroupMember")]
        [ActionName("GroupMember")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SaveGroupMember([FromBody] ChatRequest.MemberRequest request)
        {
            var response = await _service.AddMember(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Provides user with the list of group settings.
        /// </summary>
        /// <returns>List of group settings.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_GetGroupSettings")]
        [ActionName("GroupSetting")]
        [ResponseType(typeof(IEnumerable<KeyValuePair<string, string>>))]
        public async Task<HttpResponseMessage> GetGroupSettings(string groupId)
        {
            var response = await _service.GetSettings(groupId, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to delete the specified group (only by group admin) and group member can also leave and delete the specified group.
        /// </summary>
        /// <param name="request">System provided group ID.</param>
        /// <returns>The list of deleted group IDs.</returns>
        [HttpDelete]
        [MetaData("2015-02-12", markType: 3, aliasName: "Chat_GroupDelete")]
        [ActionName("Group")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Delete([FromBody]  SingleData<List<string>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.DeleteGroup(request.Data, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to request group admin to add user in the specified group.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Chat_GroupUserRequest")]
        [ActionName("GroupUserRequest")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> UserRequest([FromBody] ChatRequest.MemberRequest request)
        {
            var response = await _service.RequestUser(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Provides user with information about the group.
        /// </summary>
        /// <param name="groupId">System provided group ID.</param>
        /// <returns>Group information.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_GroupInfo")]
        [ActionName("GroupInfo")]
        [ResponseType(typeof(ChatResponse.ChatGroupResponse))]
        public async Task<HttpResponseMessage> GetGroupInfo(string groupId)
        {
            var response = await _service.GetGroupInfo(groupId, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Provides user with the list of groups.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>List of groups.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_GroupPull")]
        [ActionName("GroupPull")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> GroupPull([FromBody] ChatRequest.ChatGroupPullRequest request)
        {
            var response = await _service.GroupPull(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, response.Data);
        }

        /// <summary>
        /// Provides user with the list of groups asynchronously.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>List of groups.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_TinyGroupPush")]
        [ActionName("TinyGroupPush")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> TinyGroupPush([FromBody] ChatRequest.ChatGroupPullRequest request)
        {
            var response = await _service.TinyGroupPush(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with the updated routing list. 
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The updated routing list.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_GearUp")]
        [ActionName("GearUp")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> GearUp([FromBody] ChatRequest.GearUpRequest request)
        {
            if (!Validation.IsEnumerablePopulated(request.InstanceList, x => request.InstanceList, ActionContext, ModelState))
                return ActionContext.Response;
            var response = await _service.GearUp(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, response.Data);
        }

        /// <summary>
        /// Provides user with the option to approve or reject a group member request (only by group admin).
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Chat_GroupMemberApproveOrReject")]
        [ActionName("GroupMemberApproveOrReject")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> GroupMemberApproveOrReject([FromBody] ChatRequest.ApproveRejectGroupRequest request)
        {
            var response = await _service.GroupMemberApproveRejectRequest(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        ///  Provides user with the list of pending group members.
        /// </summary>
        /// <param name="groupId">System provided group ID.</param>
        /// <returns>List of pending group members.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_PendingGroupMember")]
        [ActionName("PendingGroupMember")]
        [ResponseType(typeof(IEnumerable<ChatResponse.ChatPendingGroupMember>))]
        public async Task<HttpResponseMessage> PendingGroupMember(string groupId)
        {
            var response = await _service.GetPendingGroupMember(groupId, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to remove other non-admin members from the specified group (only by group admin).
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPut]
        [MetaData(markType: 3, aliasName: "Chat_GroupUserRemove")]
        [ActionName("GroupUserRemove")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> GroupUserRemove([FromBody] ChatRequest.MemberRequest request)
        {
            var response = await _service.RemoveUser(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        ///  Provides user with the history of a particular group.
        /// </summary>
        /// <param name="groupId">System provided group ID.</param>
        /// <returns>History of the group.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Chat_GroupHistory")]
        [ActionName("GroupHistory")]
        [ResponseType(typeof(IEnumerable<ChatResponse.ChatPendingGroupMember>))]
        public async Task<HttpResponseMessage> GroupHistory(string groupId)
        {
            var response = await _service.GetGroupHistory(groupId, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows user to move instances to a particular inbox.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The instance IDs of moved instances.</returns>
        [HttpPost]
        [MetaData("2015-02-02", markType: 3, aliasName: "Chat_MoveToInbox")]
        [ActionName("MoveToInbox")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody]ChatRequest.MoveToInboxRequest request)
        {
            if (!Validation.IsEnumerablePopulated(request.InstanceList, x => request.InstanceList, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.MoveToInbox(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to update view count and view time limit of the disposable instance.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-02-10", markType: 3, aliasName: "Chat_UpdateDisposableInstance")]
        [ActionName("UpdateDisposableInstance")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody]ChatRequest.DisposableInstanceRequest request)
        {
            var response = await _service.UpdateDisposableInstance(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to search the message.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>Message search response.</returns>
        [HttpPost]
        [MetaData("2015-03-26", markType: 3, aliasName: "Chat_Search")]
        [ActionName("Search")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody]ChatRequest.SearchRequest request)
        {
            var response = await _service.Search(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Allows user to update the push code.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-03-26", markType: 3, aliasName: "Chat_UpdatePushCode")]
        [ActionName("UpdatePushCode")]
        [ResponseType(typeof(ChatResponse.SearchResponse))]
        public async Task<HttpResponseMessage> UpdatePushCode([FromBody]SingleData<string> request)
        {
            var response = await _service.UpdatePushCode(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }
    }
}
