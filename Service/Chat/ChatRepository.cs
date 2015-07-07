using Model.Chat;
using Model.Common;
using Model.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Chat
{
    public class ChatRepository : IChatRepository
    {
        protected readonly Services Client;

        public ChatRepository(Services client)
        {
            Client = client;
        }

        public async Task<StatusData<object>> MessagePull(ChatRequest.MessageRequest request, SystemSession session)
        {
            var result = new StatusData<object> { Status = SystemDbStatus.Selected };

            var chatMessagePull = new ChatMessagePull { UserId = session.UserName, PageSize = request.PageSize, DrillUp = request.DrillUp, DrillDown = request.DrillDown, Limit = request.Limit, InstanceId = request.InstanceId, LastMsg = request.LastMessage, Cursor = request.Cursor, UnreadMsg = request.UnReadMessage };

            var response = await Task.Factory.StartNew(() => Client.ChatService.msgPull(chatMessagePull, session.GetSession())).ConfigureAwait(false);
            Helper.Helper.ExtractData(result, response, "error", elementsToBypass: new Collection<string> { "error" });
            return result;
        }

        public async Task<object> TinyMessagePush(ChatRequest.MessageRequest request, SystemSession session)
        {
            // var result = new StatusData<object> { Status = SystemDbStatus.Selected };

            var tinyMessagePull = new ChatMessagePull { UserId = session.UserName, PageSize = request.PageSize, DrillUp = request.DrillUp, DrillDown = request.DrillDown, Limit = request.Limit, InstanceId = request.InstanceId, LastMsg = request.LastMessage, Cursor = request.Cursor, UnreadMsg = request.UnReadMessage };

            var response = await Task.Factory.StartNew(() => Client.ChatService.tinyMsgPush(tinyMessagePull, session.GetSession())).ConfigureAwait(false);
            // Helper.Helper.ExtractData(result, response, "error", elementsToBypass: new Collection<string> { "error" });
            return JsonConvert.DeserializeObject(response);
        }

        public async Task<object> GetMessage(ChatRequest.MessageInformationRequest request, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatUserInfoService.getMessage(request.InstanceId, request.MessageId, session.GetSession())).ConfigureAwait(false);
            return JsonConvert.DeserializeObject(response);
        }

        public async Task<List<string>> MessageDelete(ChatRequest.MessageDeleteRequest request, SystemSession session)
        {
            return await Task.Factory.StartNew(() => Client.ChatUserInfoService.msgDelete(request.InstanceId, request.MessageList, session.GetSession())).ConfigureAwait(false);
        }

        public async Task<StatusData<object>> InstancePull(ChatRequest.InstanceRequest request, SystemSession session)
        {
            var result = new StatusData<object> { Status = SystemDbStatus.Selected };

            if (request.FolderId == null)
                request.FolderId = "0";

            var instancePull = new ChatInstancePullInfo { UserId = session.UserName, PageSize = request.PageSize, DrillUp = request.DrillUp, DrillDown = request.DrillDown, Limit = request.Limit, Cursor = request.Cursor, IncludeMsg = request.IncludeMessage, FolderId = request.FolderId };

            var response = await Task.Factory.StartNew(() => Client.ChatService.instancePull(instancePull, session.GetSession())).ConfigureAwait(false);
            Helper.Helper.ExtractData(result, response, "error", elementsToBypass: new Collection<string> { "error" });

            return result;
        }

        public async Task<object> GetInstance(SingleData<string> request, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatUserInfoService.getInstance(request.Data, session.GetSession())).ConfigureAwait(false);
            return JsonConvert.DeserializeObject(response);
        }

        public async Task<object> TinyInstancePush(ChatRequest.InstanceRequest request, SystemSession session)
        {
            var tinyInstancePush = new ChatInstancePullInfo { UserId = session.UserName, PageSize = request.PageSize, DrillUp = request.DrillUp, DrillDown = request.DrillDown, Limit = request.Limit, Cursor = request.Cursor, IncludeMsg = request.IncludeMessage, FolderId = request.FolderId };

            var response = await Task.Factory.StartNew(() => Client.ChatService.tinyInstancePush(tinyInstancePush, session.GetSession())).ConfigureAwait(false);

            return JsonConvert.DeserializeObject(response);
        }

        public async Task<StatusData<string>> InstanceOperation(ChatRequest.InstanceOperationRequest request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.instanceOperation(request.InstanceList, (InstanceOperation)(request.Mode), session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Updated };

            return result;
        }

        //public async Task<StatusData<string>> SaveGroup(GroupUserRequest request, SystemSession session)
        //{
        //    request.Users.Add(session.UserName);
        //    await Task.Factory.StartNew(() => _client.ChatUserInfoService.saveGroupForUsers(request.GroupGuid, request.Users.Distinct().ToList(), session.GetSession()));
        //    var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
        //    return result;
        //}

        //public async Task<IEnumerable<string>> GetGroups(SystemSession session)
        //{
        //    var response = await Task.Factory.StartNew(() => _client.ChatUserInfoService.getGroups(session.UserName, session.GetSession()));
        //    return response;
        //}

        public async Task<StatusData<string>> BlockUnBlockInstances(SingleData<List<string>> request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.saveBlockedInstanceIds(request.Data, session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<StatusData<string>> MuteUnMuteInstances(SingleData<List<string>> request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.saveMutedInstanceIds(request.Data, session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<StatusData<string>> BlockUsers(ChatRequest.BlockUsersRequest request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.saveUserBlockedInstancess(request.InstanceId, request.Users, session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<StatusData<string>> MuteUsers(SingleData<List<string>> request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.saveMuteUserIds(request.Data, session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<IEnumerable<ChatResponse.GroupInstanceResponse>> GetBlockedInstances(SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatUserInfoService.getBlockedInstances(session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new ChatResponse.GroupInstanceResponse
            {
                InstanceId = x.InstanceId,
                TimeStamp = x.Timestamp
            });
            return result;
        }

        public async Task<IEnumerable<ChatResponse.UserBlockedInstancesResponse>> GetUserBlockedInstances(SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatUserInfoService.getUserBlockedInstances(session.UserName, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new ChatResponse.UserBlockedInstancesResponse
            {
                InstanceId = x.InstanceId,
                BlockedUsers = x.BlockedUsers.Select(y => new ChatResponse.ChatUserBlockedResponse
                {
                    UserId = y.UserId,
                    TimeStamp = y.Timestamp
                }).ToList()
            });
            return result;
        }

        public async Task<IEnumerable<ChatResponse.GroupInstanceResponse>> GetMutedInstances(SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatUserInfoService.getMutedInstances(session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new ChatResponse.GroupInstanceResponse
            {
                InstanceId = x.InstanceId,
                TimeStamp = x.Timestamp
            });
            return result;
        }

        public async Task<IEnumerable<ChatResponse.ChatUserBlockedResponse>> GetMutedUsers(SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatUserInfoService.getMutedUsers(session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new ChatResponse.ChatUserBlockedResponse
            {
                UserId = x.UserId,
                TimeStamp = x.Timestamp
            });
            return result;
        }

        public async Task<StatusData<string>> GroupAdd(ChatRequest.ChatGroupRequest request, SystemSession session)
        {
            var chatGroupObject = new ChatGroup
            {
                GroupName = request.GroupName,
                CreatedBy = session.UserName,
                CreatedDate = request.CreatedDate,
                Members = request.Members,
                // GroupSettings = request.GroupSettings//.Select(x => new ChatGroupSetting { Key = x.Key, Value = x.Value }).ToList()
                GroupSettings = "[{" + string.Join("}, {", request.GroupS.Select(x => string.Format("\"k\": \"{0}\", \"v\": \"{1}\"", x.K, x.V))) + "}]"
            };
            var response = await Task.Factory.StartNew(() => Client.ChatGroupService.addGroup(chatGroupObject, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted, Data = response };
            return result;
        }

        public async Task<StatusData<IEnumerable<string>>> GetGroupMember(string groupId, SystemSession session)
        {
            var result = new StatusData<IEnumerable<string>>
            {
                Data = await Task.Factory.StartNew(() => Client.ChatGroupService.getGroupMember(groupId, session.UserName, session.GetSession())).ConfigureAwait(false),
                Status = SystemDbStatus.Selected
            };
            return result;
        }

        public async Task<StatusData<List<string>>> LeaveGroup(List<string> groupId, SystemSession session)
        {
            var result = new StatusData<List<string>>
            {
                Data = await Task.Factory.StartNew(() => Client.ChatGroupService.leaveGroup(session.UserName, groupId, session.GetSession())).ConfigureAwait(false),
                Status = SystemDbStatus.Updated
            };
            return result;
        }

        public async Task<StatusData<string>> UpdateGroupName(ChatRequest.ChatGroupRequest request, string groupId, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatGroupService.updateGroupName(groupId, request.GroupName, session.UserName, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Updated };
            return result;
        }

        public async Task<StatusData<string>> AddGroupSetting(ChatRequest.GroupSettingRequest request, SystemSession session)
        {
            var chatGroupSettingObject = "[{" + string.Join("}, {", request.GroupS.Select(x => string.Format("\"k\": \"{0}\", \"v\": \"{1}\"", x.K, x.V))) + "}]";
            await Task.Factory.StartNew(() => Client.ChatGroupService.addSettings(session.UserName, request.GroupId, chatGroupSettingObject, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<StatusData<string>> AddMember(ChatRequest.MemberRequest request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatGroupService.addMembers(session.UserName, request.GroupId, request.Users, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<object> GetSettings(string groupId, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject(Client.ChatGroupService.getSettings(session.UserName, groupId, session.GetSession()))).ConfigureAwait(false);
            //var result = response.Select(x => new KeyValuePair<string, string>(x.Key, x.Value));
            return response;
        }

        public async Task<StatusData<List<string>>> DeleteGroup(List<string> groupId, SystemSession session)
        {
            var result = new StatusData<List<string>>
            {
                Data = await Task.Factory.StartNew(() => Client.ChatGroupService.deleteGroup(session.UserName, groupId, session.GetSession())).ConfigureAwait(false),
                Status = SystemDbStatus.Deleted
            };
            return result;
        }

        public async Task<StatusData<string>> RequestUser(ChatRequest.MemberRequest request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatGroupService.requestUser(session.UserName, request.GroupId, request.Users, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<StatusData<ChatResponse.ChatGroupResponse>> GetGroupInfo(string groupId, SystemSession session)
        {
            var response = new StatusData<ChatResponse.ChatGroupResponse> { Status = SystemDbStatus.Selected };
            var serviceResult = await Task.Factory.StartNew(() => Client.ChatGroupService.getGroupInfo(groupId, session.UserName, session.GetSession())).ConfigureAwait(false);
            if (serviceResult.GroupId == null)
            {
                response.Status = SystemDbStatus.NotFound;
                return response;
            }
            response.Data = new ChatResponse.ChatGroupResponse
           {
               GroupId = serviceResult.GroupId,
               GroupName = serviceResult.GroupName,
               CreatedBy = serviceResult.CreatedBy,
               CreatedDate = serviceResult.CreatedDate,
               Members = serviceResult.Members,
               GroupS = JsonConvert.DeserializeObject(serviceResult.GroupSettings),
               PendingGroupMember = serviceResult.PendingGroupMember.Select(x => new ChatResponse.ChatPendingGroupMember
               {
                   User = x.UserId,
                   RequestedByUser = x.RequestedByUserId,
                   RequestedTimeStamp = x.RequestedTimestamp,
                   ApprovedTimeStamp = x.ApprovedTimestamp,
                   LeftTimeStamp = x.LeftTimestamp,
                   DeletedTimeStamp = x.DeletedTimestamp
               }).ToList()
           };

            return response;
        }

        public async Task<StatusData<object>> GroupPull(ChatRequest.ChatGroupPullRequest request, SystemSession session)
        {
            var result = new StatusData<object> { Status = SystemDbStatus.Selected };

            var groupPull = new GroupPull { UserId = session.UserName, PageSize = request.PageSize, DrillUp = request.DrillUp, DrillDown = request.DrillDown, Limit = request.Limit, IncludeInstance = request.IncludeInstance, Cursor = request.Cursor };

            var response = await Task.Factory.StartNew(() => Client.ChatService.groupPull(groupPull, session.GetSession())).ConfigureAwait(false);

            Helper.Helper.ExtractData(result, response, "error", elementsToBypass: new Collection<string> { "error" });
            return result;
        }

        public async Task<object> TinyGroupPush(ChatRequest.ChatGroupPullRequest request, SystemSession session)
        {
            var tinyGroupPush = new GroupPull { UserId = session.UserName, PageSize = request.PageSize, DrillUp = request.DrillUp, DrillDown = request.DrillDown, Limit = request.Limit, IncludeInstance = request.IncludeInstance, Cursor = request.Cursor };

            var response = await Task.Factory.StartNew(() => Client.ChatService.tinyGroupPush(tinyGroupPush, session.GetSession())).ConfigureAwait(false);

            return JsonConvert.DeserializeObject(response);
        }

        public async Task<StatusData<object>> GearUp(ChatRequest.GearUpRequest request, SystemSession session)
        {
            var result = new StatusData<object> { Status = SystemDbStatus.Selected };

            var gearUpObject = new GearUp { UserId = session.UserName, Timestamp = request.TimeStamp, InstanceList = request.InstanceList };

            var response = await Task.Factory.StartNew(() => Client.ChatService.gearUpVer2(gearUpObject, session.GetSession())).ConfigureAwait(false);

            //Helper.Helper.ExtractData(result, response, "error", elementsToBypass: new Collection<string> { "error" });

            result.Data = JArray.Parse(response);
            return result;

            //  return re;
        }

        public async Task<StatusData<string>> GroupMemberApproveRejectRequest(ChatRequest.ApproveRejectGroupRequest request, SystemSession session)
        {
            var memberApproveRejectRequest = new ChatGroup { GroupId = request.GroupId, Mar = JsonConvert.SerializeObject(request.Mar, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }) };
            await Task.Factory.StartNew(() => Client.ChatGroupService.approveGroupRequestByAdmin(session.UserName, memberApproveRejectRequest, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Updated };
            return result;
        }

        public async Task<IEnumerable<ChatResponse.ChatPendingGroupMember>> GetPendingGroupMember(string groupId, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatGroupService.getPendingList(session.UserName, groupId, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new ChatResponse.ChatPendingGroupMember
            {
                User = x.UserId,
                ApprovedTimeStamp = x.ApprovedTimestamp,
                DeletedTimeStamp = x.DeletedTimestamp,
                LeftTimeStamp = x.LeftTimestamp,
                RequestedByUser = x.RequestedByUserId,
                RequestedTimeStamp = x.RequestedTimestamp
            });
            return result;
        }

        public async Task<StatusData<string>> RemoveUser(ChatRequest.MemberRequest request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatGroupService.removeUser(session.UserName, request.GroupId, request.Users, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Inserted };
            return result;
        }

        public async Task<IEnumerable<ChatResponse.ChatPendingGroupMember>> GetGroupHistory(string groupId, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ChatGroupService.getGroupHistory(session.UserName, groupId, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new ChatResponse.ChatPendingGroupMember
            {
                User = x.UserId,
                ApprovedTimeStamp = x.ApprovedTimestamp,
                DeletedTimeStamp = x.DeletedTimestamp,
                LeftTimeStamp = x.LeftTimestamp,
                RequestedByUser = x.RequestedByUserId,
                RequestedTimeStamp = x.RequestedTimestamp
            });
            return result;
        }

        public async Task<StatusData<string>> ChatMemberAddRemove(ChatRequest.BlockUsersRequest request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.chatMemAddRem(request.InstanceId, request.Users, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Updated };
            return result;
        }

        public async Task<StatusData<List<string>>> MoveToInbox(ChatRequest.MoveToInboxRequest request, SystemSession session)
        {
            var result = new StatusData<List<string>>
            {
                Data = await Task.Factory.StartNew(() => Client.InboxService.moveToInbox(request.FromFolderId, request.ToFolderId, request.InstanceList, session.GetSession())).ConfigureAwait(false),
                Status = SystemDbStatus.Updated
            };
            return result;
        }

        public async Task<StatusData<string>> UpdateDisposableInstance(ChatRequest.DisposableInstanceRequest request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.updateDisposableInstance(request.InstanceId, request.ViewCount, request.ViewTimeLimit, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Updated };
            return result;
        }

        //public async Task<StatusData<object>> MessagePull(ChatRequest.MessageRequest request, SystemSession session)
        //{
        //    var result = new StatusData<object> { Status = SystemDbStatus.Selected };

        //    var chatMessagePull = new ChatMessagePull { UserId = session.UserName, PageSize = request.PageSize, DrillUp = request.DrillUp, DrillDown = request.DrillDown, Limit = request.Limit, InstanceId = request.InstanceId, LastMsg = request.LastMessage, Cursor = request.Cursor, UnreadMsg = request.UnReadMessage };

        //    var response = await Task.Factory.StartNew(() => _client.ChatService.msgPull(chatMessagePull, session.GetSession())).ConfigureAwait(false);
        //    ExtractData(result, response, "error", elementsToBypass: new Collection<string> { "error" });
        //    return result;
        //}

        public async Task<StatusData<object>> Search(ChatRequest.SearchRequest request, SystemSession session)
        {
            var serviceRequest = new SearchQuery
            {
                Query = request.Query,
                InstanceId = request.InstanceId,
                Start = request.Start,
                Limit = request.Limit
            };
            var response = await Task.Factory.StartNew(() => Client.SearchService.query(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<object> { Status = SystemDbStatus.Selected };
            Helper.Helper.ExtractData(result, JObject.FromObject(new { Response = JsonConvert.DeserializeObject<object>(response.Response), response.Channel, response.Error }), "Error", elementsToBypass: new Collection<string> { "Error" });
            return result;
        }

        public async Task<StatusData<string>> UpdatePushCode(SingleData<string> request, SystemSession session)
        {
            await Task.Factory.StartNew(() => Client.ChatUserInfoService.updatePushCode(request.Data, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = SystemDbStatus.Updated };
            return result;
        }
    }
}
