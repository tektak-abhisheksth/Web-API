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
    
    public partial class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
            this.Locations = new HashSet<Location>();
            this.UserInfoes = new HashSet<UserInfo>();
        }
    
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
    
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
