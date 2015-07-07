using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Chat
{
    public class ChatResponse
    {
        public class GroupInstanceResponse
        {
            [Description("Unique id of the instance.")]
            public string InstanceId { get; set; }
            public long TimeStamp { get; set; }
        }

        public class ChatUserBlockedResponse
        {
            [Description("Username of user.")]
            public string UserId { get; set; }
            public long TimeStamp { get; set; }
        }

        public class UserBlockedInstancesResponse
        {
            [Description("Unique id of the instance.")]
            public string InstanceId { get; set; }
            public List<ChatUserBlockedResponse> BlockedUsers { get; set; }
        }

        public class ChatGroupResponse
        {
            [Description("Unique id of the group.")]
            public string GroupId { get; set; }
            [Description("Name of the group.")]
            public string GroupName { get; set; }
            [Description("User name who created the group.")]
            public string CreatedBy { get; set; }
            [Description("Timestamp of group creation.")]
            public long CreatedDate { get; set; }
            [Description("List of user names who are related to group.")]
            public IEnumerable<string> Members { get; set; }
            [Description("Settings information in key value pair. Key will be setting name and value will be settings value.")]
            public object GroupS { get; set; }
            public IEnumerable<ChatPendingGroupMember> PendingGroupMember { get; set; }
        }

        public class ChatPendingGroupMember
        {
            [Description("Username of user.")]
            public string User { get; set; }
            public string RequestedByUser { get; set; }
            public long RequestedTimeStamp { get; set; }
            public long ApprovedTimeStamp { get; set; }
            public long LeftTimeStamp { get; set; }
            public long DeletedTimeStamp { get; set; }
        }

        public class SearchResponse
        {
            public bool Error { get; set; }
            public object Response { get; set; }
            public string Channel { get; set; }
        }
    }
}
