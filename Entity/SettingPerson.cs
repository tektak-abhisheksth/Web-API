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
    
    public partial class SettingPerson
    {
        public int UserId { get; set; }
        public byte SettingTypeId { get; set; }
        public byte Value { get; set; }
        public string ReferenceToken { get; set; }
        public Nullable<System.DateTime> StartPoint { get; set; }
        public Nullable<System.DateTime> EndPoint { get; set; }
    
        public virtual UserInfoPerson UserInfoPerson { get; set; }
        public virtual SettingTypePerson SettingTypePerson { get; set; }
    }
}
