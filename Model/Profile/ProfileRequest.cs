using Model.Attribute;
using Model.Base;
using Model.Common;
using Model.Types;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Profile
{
    public class AboutRequest : RequestBase
    {
        [Description("The headline of the biography, available on company user.")]
        public string Headline { get; set; }

        [Required, StringLength(8000, MinimumLength = 5)]
        [Description("The biography information.")]
        public string About { get; set; }
    }

    public class StatusSetRequest : RequestBase
    {
        [Required, ValidEnum]
        [Description("The status of the user.")]
        public SystemUserAvailabilityCode StatusTypeId { get; set; }

        [Description("Out of office details.")]
        public OutOfOfficeRequest OutOfOfficeRequest { get; set; }
    }

    public class OutOfOfficeRequest
    {
        [Description("The date and time the user is leaving office.")]
        public DateAndTime BeginDateAndTime { get; set; }

        [Description("The date and time the user is returning office.")]
        public DateAndTime EndDateAndTime { get; set; }

        [Description("The location of the user when out of office.")]
        public string Location { get; set; }

        [Required, ValidEnum]
        [Description("The type of reception set by the user when out of office.")]
        public SystemStatusReceptionMode ReceptionMode { get; set; }

        [Description("The user ID of the assignee.")]
        public int? AssigneeUserId { get; set; }
    }

    public class SignalViewRequest : RequestBase
    {
        [Required]
        [Description("The user name or the user ID of the target user.")]
        public string TargetUser { get; set; }

        [Description("The group ID.")]
        public int? GroupId { get; set; }

        [Required, ValidEnum]
        [Description("The profile view type.")]
        public SystemProfileViewType ViewType { get; set; }

        [Description("The unique system provided ID of the position or skill.")]
        public int? TypeId { get; set; }


    }
}
