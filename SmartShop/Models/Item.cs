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
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.PurchaseDetails = new HashSet<PurchaseDetail>();
            this.PurchaseReDetails = new HashSet<PurchaseReDetail>();
            this.SalesDetails = new HashSet<SalesDetail>();
            this.SalesReDetails = new HashSet<SalesReDetail>();
            this.StoreTransferDetails = new HashSet<StoreTransferDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CatId { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public string Company { get; set; }
        public Nullable<double> PricePur { get; set; }
        public Nullable<double> PriceSell { get; set; }
        public Nullable<double> FirstBalance { get; set; }
        public Nullable<double> FirstBalancePrice { get; set; }
        public string Description { get; set; }
        public byte[] Img { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseReDetail> PurchaseReDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesReDetail> SalesReDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreTransferDetail> StoreTransferDetails { get; set; }
    }
}
