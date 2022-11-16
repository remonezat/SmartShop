//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testreports.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public item()
        {
            this.barcodes = new HashSet<barcode>();
            this.items_pack = new HashSet<items_pack>();
            this.items_pack1 = new HashSet<items_pack>();
            this.items_units_lnks = new HashSet<items_units_lnks>();
            this.purchase_det = new HashSet<purchase_det>();
            this.purchase_re_det = new HashSet<purchase_re_det>();
            this.sales_det = new HashSet<sales_det>();
            this.sales_det_pack = new HashSet<sales_det_pack>();
            this.sales_det_pack1 = new HashSet<sales_det_pack>();
            this.sales_re_det = new HashSet<sales_re_det>();
            this.sales_re_det_pack = new HashSet<sales_re_det_pack>();
            this.sales_re_det_pack1 = new HashSet<sales_re_det_pack>();
        }
    
        public int id { get; set; }
        public string iname { get; set; }
        public Nullable<double> price_pur { get; set; }
        public Nullable<double> price1 { get; set; }
        public Nullable<double> price2 { get; set; }
        public Nullable<double> price3 { get; set; }
        public Nullable<double> price4 { get; set; }
        public string category { get; set; }
        public string company { get; set; }
        public Nullable<double> firstbalance { get; set; }
        public Nullable<double> firstbalance_val { get; set; }
        public Nullable<double> minbalance { get; set; }
        public byte[] img { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<bool> ispack { get; set; }
        public bool scale { get; set; }
        public int clr { get; set; }
        public int siz { get; set; }
        public int mdl { get; set; }
        public string unit_name { get; set; }
        public Nullable<double> unit_count { get; set; }
        public Nullable<double> firstbalance_price { get; set; }
        public Nullable<int> unit_id { get; set; }
        public Nullable<double> min_price { get; set; }
        public int sortin_cate { get; set; }
        public System.DateTime lastUpdate { get; set; }
        public string ItemDiscription { get; set; }
        public int expirtion_days { get; set; }
        public int expirtion_days_alert { get; set; }
        public Nullable<int> cate_id { get; set; }
        public System.Guid msrepl_tran_version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<barcode> barcodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<items_pack> items_pack { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<items_pack> items_pack1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<items_units_lnks> items_units_lnks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<purchase_det> purchase_det { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<purchase_re_det> purchase_re_det { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_det> sales_det { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_det_pack> sales_det_pack { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_det_pack> sales_det_pack1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_re_det> sales_re_det { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_re_det_pack> sales_re_det_pack { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_re_det_pack> sales_re_det_pack1 { get; set; }
    }
}
