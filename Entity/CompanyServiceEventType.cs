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
    
    public partial class CompanyServiceEventType
    {
        public System.Guid CompanyServiceEventTypeGuid { get; set; }
        public long CompanyProductAndServiceID { get; set; }
        public int EventTypeID { get; set; }
    
        public virtual CompanyService CompanyService { get; set; }
        public virtual EventType EventType { get; set; }
    }
}