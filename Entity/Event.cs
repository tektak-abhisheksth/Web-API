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
    
    public partial class Event
    {
        public Event()
        {
            this.Requests = new HashSet<Request>();
            this.EventAttendees = new HashSet<EventAttendee>();
        }
    
        public long EventID { get; set; }
        public int UserID { get; set; }
        public byte EventType { get; set; }
        public byte Priority { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Location { get; set; }
        public System.Data.Entity.Spatial.DbGeography Point { get; set; }
        public bool AllowAttendeesToInvite { get; set; }
        public string OriginatorType { get; set; }
        public Nullable<long> OriginatorTypeTarget { get; set; }
        public string RecType { get; set; }
        public Nullable<long> EventPID { get; set; }
        public System.DateTime Created { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual EventDetail EventDetail { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<EventAttendee> EventAttendees { get; set; }
    }
}