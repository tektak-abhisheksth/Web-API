using Model.Attribute;
using Model.Base;
using Model.Common;
using Model.Types;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Profile
{
    public class AboutResponse
    {
        [Description("The headline of the biography, available on company user.")]
        public string Headline { get; set; }

        [Description("The biography information.")]
        public string About { get; set; }
    }

    public class OutOfOfficeResponse
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

        [Description("The user information of the assignee.")]
        public Assignee Assignee { get; set; }
    }

    public class Assignee : UserInformationBaseExtendedResponse
    {
        [Description("The primary contact number of the assignee.")]
        public string CallNumber { get; set; }

        [Description("An indication as to whether or not the assignee is available for message reception or not.")]
        public bool AvailableForMessage { get; set; }

        [Description("The status of the assignee.")]
        public SystemUserAvailabilityCode StatusTypeId { get; set; }

        [Description("The type of reception set by the assignee when out of office.")]
        public SystemStatusReceptionMode ReceptionMode { get; set; }
    }

    public class UserStatusResponse
    {
        [Description("The status of the user.")]
        public SystemUserAvailabilityCode StatusTypeId { get; set; }

        [Description("The out of office details.")]
        public OutOfOfficeResponse OutOfOffice { get; set; }
    }
}
