using System;
using System.ComponentModel;

namespace Model.Common
{
    public class BeginEndDate
    {
        [Description("The start date.")]
        public DateTime BeginDate { get; set; }

        [Description("The end date.")]
        public DateTime? EndDate { get; set; }
    }

    public class BeginEndTime
    {
        [Description("The start time.")]
        public TimeSpan? BeginTime { get; set; }

        [Description("The end time.")]
        public TimeSpan? EndTime { get; set; }
    }

    public class DateAndTime
    {
        [Description("The date.")]
        public DateTime? Date { get; set; }

        [Description("The time.")]
        public TimeSpan? Time { get; set; }
    }
}
