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
    
    public partial class FNCONTACTSDETAILS_Result
    {
        public int UserID { get; set; }
        public long ContactID { get; set; }
        public byte ContactTypeID { get; set; }
        public string Address { get; set; }
        public bool AddressVisible { get; set; }
        public string Email { get; set; }
        public bool EmailVisible { get; set; }
        public string Fax { get; set; }
        public bool FaxVisible { get; set; }
        public string Mobile { get; set; }
        public bool MobileVisible { get; set; }
        public string Phone { get; set; }
        public bool PhoneVisible { get; set; }
        public string Website { get; set; }
        public bool WebsiteVisible { get; set; }
        public Nullable<int> CompanyUserID { get; set; }
        public bool CompanyUserIDVisible { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string StrFixedFieldSuggestions { get; set; }
        public string StrChatNetworks { get; set; }
        public string StrChatNetworkSuggestions { get; set; }
        public string StrContactCustoms { get; set; }
        public string StrContactCustomSuggestions { get; set; }
    }
}
