using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Model.Base;

namespace Model.Notification
{
    public class NotificationRequest
    {
        [Required]
        [Description("A filter that specifies that response should be of only given type. 0 indicates notifications only, 1 indicates requests only, and any value greater than 1 indicates both notifications and requests.")]
        public byte FilterType { get; set; }

        [IgnoreDataMember]
        [Required, Range(-1, 1)]
        [Description("A direction specifier that specifies whether sent or received requests should be returned. 0 indicates received requests only, 1 indicates sent requests only, and -1 indicates both. Direction is ignored for notifications.")]
        public int RequestDirection { get; set; }

        [Description("An optional list of one or more notification or request type IDs. Only the request/notification of provided types will be returned.")]
        public List<int> NotificationRequestTypes { get; set; }

        [IgnoreDataMember]
        [Description("An optional request/notification request parameter. Information related to the provided notification ID or request ID will be returned.")]
        public long? NotificationRequestId { get; set; }
    }

    public class SetNotificationInternal : RequestBase
    {
       public List<int> NotificationId { get; set; }
        public int FilterType { get; set; }
        public bool IsClicked { get; set; }
    }
}
