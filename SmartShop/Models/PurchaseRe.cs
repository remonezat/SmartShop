//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseRe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseRe()
        {
            this.PurchaseReDetails = new HashSet<PurchaseReDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> AccId { get; set; }
        public Nullable<int> StkId { get; set; }
        public Nullable<int> ShiftId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<bool> IsCash { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> Descount { get; set; }
        public Nullable<double> Final { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseReDetail> PurchaseReDetails { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual User User { get; set; }
    }
}
