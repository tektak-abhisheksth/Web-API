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
    
    public partial class CompanyServiceTimeType
    {
        public CompanyServiceTimeType()
        {
            this.CompanyServices = new HashSet<CompanyService>();
        }
    
        public int CompanyServiceTimeTypeID { get; set; }
        public string Time { get; set; }
        public int TimeInMinutes { get; set; }
    
        public virtual ICollection<CompanyService> CompanyServices { get; set; }
    }
}