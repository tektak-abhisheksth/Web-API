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
    
    public partial class ThumbsForUserSkill
    {
        public string SkillGUID { get; set; }
        public int ThumbsProviderID { get; set; }
        public bool UpOrDown { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
    }
}
