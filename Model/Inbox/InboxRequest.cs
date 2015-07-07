using Model.Attribute;
using Model.Base;
using Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Model.Inbox
{
    public class InboxRequest : RequestBase
    {
        [Required, StringLength(20, MinimumLength = 1), RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "Inbox name contains invalid character(s).")]
        [Description("The name of the inbox.")]
        public string Name { get; set; }

        [Description("The list of rules for the inbox.")]
        public List<InboxRuleRequest> Rule { get; set; }
    }

    public class InboxMuteRequest : RequestBase
    {
        [Description("The individually-unique list of inbox folder IDs of the user.")]
        public List<int> FolderList { get; set; }

        [Description("An indication as to whether or not the folders' notifications should be muted.")]
        public bool Mute { get; set; }
    }
    public class InboxRuleRequest : IEquatable<InboxRuleRequest>
    {
        [ValidEnum]
        [Description("The selection of contacts or groups for rule.")]
        public SystemUserSelection UserSelection { get; set; }

        [ValidEnum]
        [Description("The selection of rule criterion for user or group based off the selection of contacts for rule selection.")]
        public SystemRuleTypeUser RuleTypeUser { get; set; }

        [Description("The list of contacts for the rule.")]
        public List<string> ContactList { get; set; }

        [Description("The list of groups for the rule.")]
        public List<string> GroupList { get; set; }

        //  [Required, Range(0, 2)]
        [ValidEnum]
        [Description("The selection of rule criterion for subject for rule selection.")]
        public SystemRuleTypeSubject RuleTypeSubject { get; set; }

        [StringLength(255, MinimumLength = 2)]
        [Description("The subject.")]
        public string Subject { get; set; }

        [Description("An indication as to whether or not the rule should be applied on existing message instances.")]
        public bool ApplyOnOldMessage { get; set; }

        public bool Equals(InboxRuleRequest other)
        {
            var listsAreEqual = false;
            switch (UserSelection)
            {
                case SystemUserSelection.Contacts:
                    listsAreEqual = !ContactList.Except(other.ContactList).Union(other.ContactList.Except(ContactList)).Any();
                    break;
                case SystemUserSelection.Groups:
                    listsAreEqual = !GroupList.Except(other.GroupList).Union(other.GroupList.Except(GroupList)).Any();
                    break;
            }
            return UserSelection == other.UserSelection && RuleTypeUser == other.RuleTypeUser && RuleTypeSubject == other.RuleTypeSubject && Subject == other.Subject && listsAreEqual;
        }

        public override int GetHashCode()
        {
            return ((int)UserSelection) ^ ((int)RuleTypeUser) ^ ((int)RuleTypeSubject) ^ (Subject ?? string.Empty).GetHashCode();
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}+{1}+{2}+{3}+{4}+{5}", UserSelection, UserSelection.Equals((byte)SystemUserSelection.None) ? 0 : RuleTypeUser,
        //        ContactList != null && UserSelection.Equals((byte)SystemUserSelection.Contacts) ? string.Join(",", ContactList) : string.Empty,
        //        GroupList != null && UserSelection.Equals((byte)SystemUserSelection.Groups) ? string.Join(",", GroupList) : string.Empty, RuleTypeSubject,
        //        Subject);
        //}
    }

    public class RuleAddRequest : RequestBase
    {
        [Required, Range(1, int.MaxValue)]
        [Description("The individually-unique inbox folder ID of the user.")]
        public int FolderId { get; set; }

        [Description("The rule information.")]
        public InboxRuleRequest Rule { get; set; }
    }

    public class RuleUpdateRequest : RuleAddRequest
    {
        [Required, Range(1, long.MaxValue)]
        [Description("The unique inbox rule ID of the user.")]
        public long MessageRuleId { get; set; }
    }
}
