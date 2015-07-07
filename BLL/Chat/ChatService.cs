using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Chat;
using Model.Common;
using Model.Types;
using TekTak.iLoop.UOW;

namespace BLL.Chat
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _jUnitOfWork;

        public ChatService(IUnitOfWork jUnitOfWork)
        {

            _jUnitOfWork = jUnitOfWork;
        }
        public Task<StatusData<object>> MessagePull(ChatRequest.MessageRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.MessagePull(request, session);
        }

        public Task<object> TinyMessagePush(ChatRequest.MessageRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.TinyMessagePush(request, session);
        }
        public Task<List<string>> MessageDelete(ChatRequest.MessageDeleteRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.MessageDelete(request, session);
        }
        public Task<StatusData<Object>> InstancePull(ChatRequest.InstanceRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.InstancePull(request, session);
        }

        public Task<object> TinyInstancePush(ChatRequest.InstanceRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.TinyInstancePush(request, session);
        }

        public Task<StatusData<string>> InstanceOperation(ChatRequest.InstanceOperationRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.InstanceOperation(request, session);
        }
        //public Task<StatusData<string>> SaveGroup(GroupUserRequest request, SystemSession session)
        //{
        //    return _jUnitOfWork.Message.SaveGroup(request, session);
        //}

        //public Task<IEnumerable<string>> GetGroups(SystemSession session)
        //{
        //    return _jUnitOfWork.Message.GetGroups(session);
        //}

        public Task<StatusData<string>> BlockUnBlockInstances(SingleData<List<string>> request, SystemSession session)
        {
            return _jUnitOfWork.Chat.BlockUnBlockInstances(request, session);
        }

        public Task<StatusData<string>> MuteUnMuteInstances(SingleData<List<string>> request, SystemSession session)
        {
            return _jUnitOfWork.Chat.MuteUnMuteInstances(request, session);
        }

        public Task<StatusData<string>> BlockUsers(ChatRequest.BlockUsersRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.BlockUsers(request, session);
        }

        public Task<StatusData<string>> MuteUsers(SingleData<List<string>> request, SystemSession session)
        {
            return _jUnitOfWork.Chat.MuteUsers(request, session);
        }

        public Task<IEnumerable<ChatResponse.GroupInstanceResponse>> GetBlockedInstances(SystemSession session)
        {
            return _jUnitOfWork.Chat.GetBlockedInstances(session);
        }

        public Task<IEnumerable<ChatResponse.UserBlockedInstancesResponse>> GetUserBlockedInstances(SystemSession session)
        {
            return _jUnitOfWork.Chat.GetUserBlockedInstances(session);
        }

        public Task<IEnumerable<ChatResponse.GroupInstanceResponse>> GetMutedInstances(SystemSession session)
        {
            return _jUnitOfWork.Chat.GetMutedInstances(session);
        }

        public Task<IEnumerable<ChatResponse.ChatUserBlockedResponse>> GetMutedUsers(SystemSession session)
        {
            return _jUnitOfWork.Chat.GetMutedUsers(session);
        }

        public Task<StatusData<string>> GroupAdd(ChatRequest.ChatGroupRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.GroupAdd(request, session);
        }

        public Task<StatusData<IEnumerable<string>>> GetGroupMember(string groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.GetGroupMember(groupId, session);
        }

        public Task<StatusData<List<string>>> LeaveGroup(List<string> groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.LeaveGroup(groupId, session);
        }

        public Task<StatusData<string>> UpdateGroupName(ChatRequest.ChatGroupRequest request, string groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.UpdateGroupName(request, groupId, session);
        }

        public Task<StatusData<string>> AddGroupSetting(ChatRequest.GroupSettingRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.AddGroupSetting(request, session);
        }

        public Task<StatusData<string>> AddMember(ChatRequest.MemberRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.AddMember(request, session);
        }

        public Task<object> GetSettings(string groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.GetSettings(groupId, session);
        }

        public Task<StatusData<List<string>>> DeleteGroup(List<string> groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.DeleteGroup(groupId, session);
        }

        public Task<StatusData<string>> RequestUser(ChatRequest.MemberRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.RequestUser(request, session);
        }

        public Task<StatusData<ChatResponse.ChatGroupResponse>> GetGroupInfo(string groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.GetGroupInfo(groupId, session);
        }

        public Task<StatusData<object>> GroupPull(ChatRequest.ChatGroupPullRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.GroupPull(request, session);
        }

        public Task<object> TinyGroupPush(ChatRequest.ChatGroupPullRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.TinyGroupPush(request, session);
        }
        public Task<StatusData<object>> GearUp(ChatRequest.GearUpRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.GearUp(request, session);
        }

        public Task<StatusData<string>> GroupMemberApproveRejectRequest(ChatRequest.ApproveRejectGroupRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.GroupMemberApproveRejectRequest(request, session);
        }

        public Task<IEnumerable<ChatResponse.ChatPendingGroupMember>> GetPendingGroupMember(string groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.GetPendingGroupMember(groupId, session);
        }

        public Task<StatusData<string>> RemoveUser(ChatRequest.MemberRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.RemoveUser(request, session);
        }

        public Task<IEnumerable<ChatResponse.ChatPendingGroupMember>> GetGroupHistory(string groupId, SystemSession session)
        {
            return _jUnitOfWork.Chat.GetGroupHistory(groupId, session);
        }

        public Task<StatusData<string>> ChatMemberAddRemove(ChatRequest.BlockUsersRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.ChatMemberAddRemove(request, session);
        }

        public Task<StatusData<List<string>>> MoveToInbox(ChatRequest.MoveToInboxRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.MoveToInbox(request, session);
        }

        public Task<object> GetInstance(SingleData<string> request, SystemSession session)
        {
            return _jUnitOfWork.Chat.GetInstance(request, session);
        }

        public Task<object> GetMessage(ChatRequest.MessageInformationRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.GetMessage(request, session);
        }

        public Task<StatusData<string>> UpdateDisposableInstance(ChatRequest.DisposableInstanceRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.UpdateDisposableInstance(request, session);
        }

        public Task<StatusData<object>> Search(ChatRequest.SearchRequest request, SystemSession session)
        {
            return _jUnitOfWork.Chat.Search(request, session);
        }

        public Task<StatusData<string>> UpdatePushCode(SingleData<string> request, SystemSession session)
        {
            return _jUnitOfWork.Chat.UpdatePushCode(request, session);
        }
    }
}
