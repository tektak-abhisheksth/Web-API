using Model.Common;
using System;
using System.ComponentModel;

namespace Model.Company.UserDepartment
{
    public class UserDepartmentResponse
    {
        [Description("The individually-unique department ID of the company along with its display name.")]
        public GeneralKvPair<int, string> Department { get; set; }

        [Description("The unique system provided ID of the assignee along with its display name.")]
        public GeneralKvPair<int, string> AssignedByUser { get; set; }

        [Description("The time when the department was added by the assignee.")]
        public DateTime Added { get; set; }
    }
}
