//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanyServiceTimeSchedule
    {
        public System.Guid CompanyServiceTimeScheduleGuid { get; set; }
        public long CompanyProductAndServiceID { get; set; }
        public byte Day { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
    
        public virtual CompanyService CompanyService { get; set; }
    }
}