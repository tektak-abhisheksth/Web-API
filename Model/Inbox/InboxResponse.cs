using Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Inbox
{
    public class InboxResponse
    {
        [Description("The individually-unique inbox folder ID of the user.")]
        public int FolderId { get; set; }

        [Description("The name of the inbox.")]
        public string Name { get; set; }

        [Description("The time when the inbox was created.")]
        public DateTime Created { get; set; }

        [Description("The number of rules for the inbox folder.")]
        public int RuleCount { get; set; }

        [Description("The mute status of the inbox.")]
        public bool Mute { get; set; }
    }

    public class RuleResponse
    {
        [Description("The unique inbox rule ID of the user.")]
        public long MessageRuleId { get; set; }

        [Description("The individually-unique inbox folder ID of the user.")]
        public int FolderId { get; set; }

        [Description("The selection of rule criterion for user or group based off the selection of contacts for rule selection.")]
        public SystemRuleTypeUser RuleTypeUser { get; set; }

        [Description("The list of contacts for the rule.")]
        public List<string> ContactList { get; set; }

        [Description("The list of groups for the rule.")]
        public List<string> GroupList { get; set; }

        [Description("The selection of rule criterion for subject for rule selection.")]
        public SystemRuleTypeSubject RuleTypeSubject { get; set; }

        [Description("The subject.")]
        public string Subject { get; set; }
    }
}
