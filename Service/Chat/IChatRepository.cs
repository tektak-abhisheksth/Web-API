using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Chat;
using Model.Common;
using Model.Types;

namespace TekTak.iLoop.Chat
{
    public interface IChatRepository
    {
        Task<StatusData<object>> MessagePull(ChatRequest.MessageRequest request, SystemSession session);
        Task<StatusData<object>> InstancePull(ChatRequest.InstanceRequest request, SystemSession session);
        //Task<StatusData<string>> SaveGroup(GroupUserRequest request, SystemSession session);
        //Task<IEnumerable<string>> GetGroups(SystemSession session);
        Task<StatusData<string>> BlockUnBlockInstances(SingleData<List<string>> request, SystemSession session);
        Task<StatusData<string>> MuteUnMuteInstances(SingleData<List<string>> request, SystemSession session);
        Task<IEnumerable<ChatResponse.GroupInstanceResponse>> GetBlockedInstances(SystemSession session);
        Task<StatusData<string>> BlockUsers(ChatRequest.BlockUsersRequest request, SystemSession session);
        Task<StatusData<string>> MuteUsers(SingleData<List<string>> request, SystemSession session);
        Task<IEnumerable<ChatResponse.UserBlockedInstancesResponse>> GetUserBlockedInstances(SystemSession session);
        Task<IEnumerable<ChatResponse.GroupInstanceResponse>> GetMutedInstances(SystemSession session);
        Task<IEnumerable<ChatResponse.ChatUserBlockedResponse>> GetMutedUsers(SystemSession session);
        Task<StatusData<string>> GroupAdd(ChatRequest.ChatGroupRequest request, SystemSession session);
        Task<StatusData<IEnumerable<string>>> GetGroupMember(string groupId, SystemSession session);
        Task<StatusData<List<string>>> LeaveGroup(List<string> groupId, SystemSession session);
        Task<StatusData<string>> UpdateGroupName(ChatRequest.ChatGroupRequest request, string groupId, SystemSession session);
        Task<StatusData<string>> AddGroupSetting(ChatRequest.GroupSettingRequest request, SystemSession session);
        Task<StatusData<string>> AddMember(ChatRequest.MemberRequest request, SystemSession session);
        Task<object> GetSettings(string groupId, SystemSession session);
        Task<StatusData<List<string>>> DeleteGroup(List<string> groupId, SystemSession session);
        Task<StatusData<string>> RequestUser(ChatRequest.MemberRequest request, SystemSession session);
        Task<StatusData<ChatResponse.ChatGroupResponse>> GetGroupInfo(string groupId, SystemSession session);
        Task<StatusData<object>> GroupPull(ChatRequest.ChatGroupPullRequest request, SystemSession session);
        Task<StatusData<object>> GearUp(ChatRequest.GearUpRequest request, SystemSession session);
        Task<StatusData<string>> GroupMemberApproveRejectRequest(ChatRequest.ApproveRejectGroupRequest request, SystemSession session);
        Task<IEnumerable<ChatResponse.ChatPendingGroupMember>> GetPendingGroupMember(string groupId, SystemSession session);
        Task<StatusData<string>> RemoveUser(ChatRequest.MemberRequest request, SystemSession session);
        Task<IEnumerable<ChatResponse.ChatPendingGroupMember>> GetGroupHistory(string groupId, SystemSession session);
        Task<StatusData<string>> InstanceOperation(ChatRequest.InstanceOperationRequest request, SystemSession session);
        Task<object> TinyMessagePush(ChatRequest.MessageRequest request, SystemSession session);
        Task<object> TinyInstancePush(ChatRequest.InstanceRequest request, SystemSession session);
        Task<object> TinyGroupPush(ChatRequest.ChatGroupPullRequest request, SystemSession session);
        Task<List<string>> MessageDelete(ChatRequest.MessageDeleteRequest request, SystemSession session);
        Task<StatusData<string>> ChatMemberAddRemove(ChatRequest.BlockUsersRequest request, SystemSession session);
        Task<StatusData<List<string>>> MoveToInbox(ChatRequest.MoveToInboxRequest request, SystemSession session);
        Task<object> GetInstance(SingleData<string> request, SystemSession session);
        Task<object> GetMessage(ChatRequest.MessageInformationRequest request, SystemSession session);
        Task<StatusData<string>> UpdateDisposableInstance(ChatRequest.DisposableInstanceRequest request, SystemSession session);
        Task<StatusData<object>> Search(ChatRequest.SearchRequest request, SystemSession session);
        Task<StatusData<string>> UpdatePushCode(SingleData<string> request, SystemSession session);
    }
}
