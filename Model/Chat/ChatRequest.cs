using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Attribute;
using Model.Base;
using Model.Common;
using Model.Types;

namespace Model.Chat
{
    public class ChatRequest
    {
        public class InstanceRequest : RequestBase
        {
            [Description("Size of total records that needs to be fetched.")]
            public int PageSize { get; set; }

            [Description("Value from where drill up will start.")]
            public string DrillUp { get; set; }

            [Description("Value from where drill down will start.")]
            public string DrillDown { get; set; }

            [Description("Value used to limit the drill.")]
            public string Limit { get; set; }

            // [Description("Value used to limit the drill.")]
            public bool IncludeMessage { get; set; }

            [Description("Cursor when record exceeds the page size.")]
            public string Cursor { get; set; }

            [Description("Id of inbox where instance is tagged.")]
            public string FolderId { get; set; }
        }

        public class InstanceOperationRequest : RequestBase
        {
            [Description("List of instance IDs.")]
            public List<string> InstanceList { get; set; }
            [ValidEnum]
            public SystemInstanceOperation Mode { get; set; }
        }

        public class MessageRequest : RequestBase
        {
            [Description("Size of total records that needs to be fetched.")]
            public int PageSize { get; set; }

            [Description("Value from where drill up will start.")]
            public string DrillUp { get; set; }

            [Description("Value from where drill down will start.")]
            public string DrillDown { get; set; }

            [Description("Value used to limit the drill.")]
            public string Limit { get; set; }

            [Required]
            [Description("Unique ID of the instance.")]
            public string InstanceId { get; set; }

            [Description("Last message id that client have.")]
            public string LastMessage { get; set; }

            [Description("Cursor when record exceeds the page size.")]
            public string Cursor { get; set; }

            [Description("Value to indicate to pull unread message only.")]
            public bool UnReadMessage { get; set; }
        }

        public class MessageDeleteRequest : RequestBase
        {
            [Description("Unique ID of the instance.")]
            [Required]
            public string InstanceId { get; set; }

            [Description("List of message IDs.")]
            public List<string> MessageList { get; set; }
        }

        public class BlockUsersRequest : RequestBase
        {
            [Required]
            [Description("Unique ID of the instance.")]
            public string InstanceId { get; set; }

            [Description("List of user names.")]
            public List<string> Users { get; set; }
        }

        public class GroupSettingRequest : RequestBase
        {
            [Required]
            [Description("Unique ID of the group.")]
            public string GroupId { get; set; }

            [Description("Settings information in key value pair. Key will be setting name and value will be settings value.")]
            public List<KvPair<string, bool>> GroupS { get; set; }
        }

        public class ChatGroupRequest : RequestBase
        {
            [Required]
            [Description("Name of the group.")]
            public string GroupName { get; set; }

            [Description("Timestamp of group creation.")]
            public long CreatedDate { get; set; }

            [Description("List of user names who are related to group.")]
            public List<string> Members { get; set; }

            //public string GroupSettings { get; set; }
            [Description("Settings information in key value pair. Key will be setting name and value will be settings value.")]
            public List<KvPair<string, bool>> GroupS { get; set; }

        }

        public class MemberRequest : RequestBase
        {
            [Description("Unique ID of the group.")]
            public string GroupId { get; set; }

            [Description("List of user names.")]
            public List<string> Users { get; set; }
        }

        public class ChatGroupPullRequest : RequestBase
        {
            [Description("Size of total records that needs to be fetched.")]
            public int PageSize { get; set; }

            [Description("Value from where drill up will start.")]
            public string DrillUp { get; set; }

            [Description("Value from where drill down will start.")]
            public string DrillDown { get; set; }

            [Description("Value used to limit the drill.")]
            public string Limit { get; set; }

            public bool IncludeInstance { get; set; }

            [Description("Cursor when record exceeds the page size.")]
            public string Cursor { get; set; }
        }

        public class GearUpRequest : RequestBase
        {
            [Required]
            public long TimeStamp { get; set; }

            [Description("List of instance IDs.")]
            public List<string> InstanceList { get; set; }
        }

        public class MemberApproveRejectRequest
        {
            [Required]
            [Description("Username of user.")]
            public string UserId { get; set; }

            [Required]
            [Description("Contains value either 'r' or 'a'. 'r' means rejected and 'a' means Approved.")]
            [RegularExpression(@"^[a,r]+$", ErrorMessage = "Allowed values for request type are 'a' and 'r' only.")]
            public char RType { get; set; }
        }

        public class ApproveRejectGroupRequest : RequestBase
        {
            [Required]
            [Description("Unique id of the group.")]
            public string GroupId { get; set; }

            [Description("Used to approve or reject the member.")]
            public List<MemberApproveRejectRequest> Mar { get; set; }
        }

        public class MoveToInboxRequest : RequestBase
        {
            [Required]
            [Description("Inbox ID from where instance(s) should be moved.")]
            public string FromFolderId { get; set; }

            [Required]
            [Description("Inbox ID where instance(s) should be moved.")]
            public string ToFolderId { get; set; }

            [Description("List of instance IDs.")]
            public List<string> InstanceList { get; set; }
        }

        public class MessageInformationRequest : RequestBase
        {
            [Required]
            [Description("Unique ID of the instance.")]
            public string InstanceId { get; set; }

            [Required]
            [Description("Unique ID of the message.")]
            public string MessageId { get; set; }
        }

        public class DisposableInstanceRequest : RequestBase
        {
            [Required]
            [Description("Unique ID of the instance.")]
            public string InstanceId { get; set; }

            [Required]
            [Description("View count for disposable instance.")]
            public int ViewCount { get; set; }

            [Required]
            [Description("View time limit for disposable instance, specified in Seconds.")]
            public int ViewTimeLimit { get; set; }
        }

        public class SearchRequest : RequestBase
        {
            [Required]
            public string Query { get; set; }
            public string InstanceId { get; set; }
            public int Start { get; set; }
            public int Limit { get; set; }
        }
    }
}
