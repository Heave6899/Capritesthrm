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
    
    public partial class CityMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CityMaster()
        {
            this.EmployeeContact = new HashSet<EmployeeContact>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> StateID { get; set; }
    
        public virtual StateMaster StateMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeContact> EmployeeContact { get; set; }
    }
}
