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
    
    public partial class CompanyService
    {
        public CompanyService()
        {
            this.CompanyServiceClients = new HashSet<CompanyServiceClient>();
            this.CompanyServiceEnlistedEquipments = new HashSet<CompanyServiceEnlistedEquipment>();
            this.CompanyServiceEventTypes = new HashSet<CompanyServiceEventType>();
            this.CompanyServiceResponsibleUsers = new HashSet<CompanyServiceResponsibleUser>();
            this.CompanyServiceTimeSchedules = new HashSet<CompanyServiceTimeSchedule>();
        }
    
        public long CompanyProductAndServiceID { get; set; }
        public int CompanyServiceTimeTypeID { get; set; }
        public int Capacity { get; set; }
        public byte DeliveryTypeID { get; set; }
        public int Distance { get; set; }
    
        public virtual CompanyProductAndService CompanyProductAndService { get; set; }
        public virtual CompanyServiceTimeType CompanyServiceTimeType { get; set; }
        public virtual ICollection<CompanyServiceClient> CompanyServiceClients { get; set; }
        public virtual ICollection<CompanyServiceEnlistedEquipment> CompanyServiceEnlistedEquipments { get; set; }
        public virtual ICollection<CompanyServiceEventType> CompanyServiceEventTypes { get; set; }
        public virtual ICollection<CompanyServiceResponsibleUser> CompanyServiceResponsibleUsers { get; set; }
        public virtual ICollection<CompanyServiceTimeSchedule> CompanyServiceTimeSchedules { get; set; }
    }
}