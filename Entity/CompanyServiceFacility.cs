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
    
    public partial class CompanyServiceFacility
    {
        public CompanyServiceFacility()
        {
            this.CompanyServiceEnlistedFacilities = new HashSet<CompanyServiceEnlistedFacility>();
        }
    
        public long CompanyServiceFacilityID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<CompanyServiceEnlistedFacility> CompanyServiceEnlistedFacilities { get; set; }
        public virtual UserInfoCompany UserInfoCompany { get; set; }
    }
}
