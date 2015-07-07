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
    
    public partial class CompanyProductAndService
    {
        public CompanyProductAndService()
        {
            this.CompanyPSPrerequisites = new HashSet<CompanyPSPrerequisite>();
            this.CompanyPSPrerequisites1 = new HashSet<CompanyPSPrerequisite>();
            this.CompanyPSSeasonables = new HashSet<CompanyPSSeasonable>();
            this.CompanyPSSupplements = new HashSet<CompanyPSSupplement>();
            this.CompanyPSSupplements1 = new HashSet<CompanyPSSupplement>();
            this.CompanyServiceEnlistedFacilities = new HashSet<CompanyServiceEnlistedFacility>();
        }
    
        public long CompanyProductAndServiceID { get; set; }
        public int UserID { get; set; }
        public byte PSTypeID { get; set; }
        public bool Reservable { get; set; }
        public Nullable<int> CompanyPSCategoryID { get; set; }
        public string Name { get; set; }
        public Nullable<int> RepeatType { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public System.DateTime LastChanged { get; set; }
    
        public virtual CompanyPSCategory CompanyPSCategory { get; set; }
        public virtual UserInfoCompany UserInfoCompany { get; set; }
        public virtual ICollection<CompanyPSPrerequisite> CompanyPSPrerequisites { get; set; }
        public virtual ICollection<CompanyPSPrerequisite> CompanyPSPrerequisites1 { get; set; }
        public virtual CompanyPSPrice CompanyPSPrice { get; set; }
        public virtual CompanyPSPrice CompanyPSPrice1 { get; set; }
        public virtual ICollection<CompanyPSSeasonable> CompanyPSSeasonables { get; set; }
        public virtual ICollection<CompanyPSSupplement> CompanyPSSupplements { get; set; }
        public virtual ICollection<CompanyPSSupplement> CompanyPSSupplements1 { get; set; }
        public virtual CompanyService CompanyService { get; set; }
        public virtual ICollection<CompanyServiceEnlistedFacility> CompanyServiceEnlistedFacilities { get; set; }
    }
}
