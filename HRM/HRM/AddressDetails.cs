//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRM
{
    using System;
    using System.Collections.Generic;
    
    public partial class AddressDetails
    {
        public int Id { get; set; }
        public string Address_line_1 { get; set; }
        public string Address_line_2 { get; set; }
        public string Address_line_3 { get; set; }
        public int Pincode { get; set; }
        public int CompanyId { get; set; }
    
        public virtual CompanyDetails CompanyDetails { get; set; }
    }
}
