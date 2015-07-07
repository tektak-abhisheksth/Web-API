using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Model.Notification
{
    public class NotificationResponse
    {
        [Description("The system provided unique ID for the notification or request.")]
        public long NotificationId { get; set; }

        [Description("The unique system provided ID of the other (originator/target) user.")]
        public int NotifierId { get; set; }

        [Description("The unique user name of the other (originator/target) user.")]
        public string NotifierUserName { get; set; }

        [Description("The time when the notification or request was first viewed.")]
        public DateTime? ReadDate { get; set; }

        [Description("The type ID of the response. Less than 1000 indicates notification, and greater than 1000 indicates request.")]
        public int TypeId { get; set; }

        [Description("The time when the notification or request was last sent.")]
        public DateTime SentDate { get; set; }

        [Description("The time when the notification or request was first clicked.")]
        public DateTime? ClickedDate { get; set; }

        [IgnoreDataMember]
        public bool Type { get; set; }

        [IgnoreDataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public string Picture { get; set; }

        [IgnoreDataMember]
        public string Title { get; set; }

        [IgnoreDataMember]
        public string Notification { get; set; }

        [IgnoreDataMember]
        public string NrTypeId { get; set; }

        [IgnoreDataMember]
        public int MutualFriendId { get; set; }

        [IgnoreDataMember]
        public string MutualFriendName { get; set; }

        [IgnoreDataMember]
        public int MutualFriendCount { get; set; }

        [IgnoreDataMember]
        public int GroupId { get; set; }

        [IgnoreDataMember]
        public string GroupName { get; set; }

        [IgnoreDataMember]
        public int EventId { get; set; }

        [IgnoreDataMember]
        public string EventName { get; set; }
    }

    public class NotificationCountResponse
    {
        [Description("Notification Count.")]
        public int NotificationCount { get; set; }

        [Description("Request Count.")]
        public int RequestCount { get; set; }
    }
}
