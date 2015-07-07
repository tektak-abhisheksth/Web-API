using Model.Common;
using System;
using System.ComponentModel;

namespace Model.Company.EmployeeDepartment
{
    public class EmployeeDepartmentResponse
    {
        [Description("The individually-unique department ID of the company.")]
        public int DepartmentId { get; set; }

        [Description("The unique system provided ID of the employee along with his/her display name.")]
        public GeneralKvPair<int, string> Employee { get; set; }

        [Description("The time when the employee was added to the department.")]
        public DateTime Added { get; set; }
    }
}
