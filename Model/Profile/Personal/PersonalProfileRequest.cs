using Model.Attribute;
using Model.Base;
using Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Profile.Personal
{
    public class EmployeeWorkSchedule
    {
        [Required, ValidEnum]
        [Description("The type of work status.")]
        public SystemWorkSchedule ScheduleType { get; set; }

        [Description("The work schedules.")]
        public List<WorkSchedule> Schedules { get; set; }
    }

    public class EmployeeWorkScheduleUpdateRequest : EmployeeWorkSchedule
    {
        [Required]
        [Description("A unique identifier uniquely representing the entry.")]
        public long PersonEmploymentId { get; set; }
    }

    public class WorkSchedule
    {
        [Required, ValidEnum]
        [Description("The day of week.")]
        public SystemDayOfWeek Day { get; set; }

        [Required]
        [Description("The start time of the workday.")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Description("The end time of the workday.")]
        public TimeSpan EndTime { get; set; }

        public override string ToString()
        {
            return string.Format("{{{0},{1},{2}}}", (int)Day, StartTime, EndTime);
        }
    }

    public class AddEmployeeRequest : RequestBase
    {
        [Description("The user ID of the company.")]
        public int? CompanyId { get; set; }

        [Required, RegularExpression(@"^[.-_,A-Za-z0-9\s]+$"), StringLength(100, MinimumLength = 1)]
        [Description("The display name of the company.")]
        public string CompanyName { get; set; }

        [Required]
        [Description("The name of the position.")]
        public string Position { get; set; }

        [Required]
        [Description("The unique system provided ID of the city.")]
        public int CityId { get; set; }

        [Required]
        [Description("The employment start date.")]
        public DateTime StartDate { get; set; }

        [Description("The employment end date.")]
        public DateTime? EndDate { get; set; }

        [Required, ValidEnum]
        [Description("The employee's employement type.")]
        public SystemEmployeeType EmployeeTypeId { get; set; }

        [Required]
        [Description("The work schedules.")]
        public EmployeeWorkSchedule WorkSchedule { get; set; }
    }

    public class UpdateEmployeeRequest : AddEmployeeRequest
    {
        [Description("A unique identifier uniquely representing the entry.")]
        public long PersonEmploymentId { get; set; }
    }
}
