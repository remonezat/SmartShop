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
    
    public partial class sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sale()
        {
            this.sales_det_pack = new HashSet<sales_det_pack>();
            this.sales_det = new HashSet<sales_det>();
        }
    
        public int id { get; set; }
        public Nullable<int> date { get; set; }
        public Nullable<int> acc_id { get; set; }
        public Nullable<bool> isCash { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<double> descount_cash { get; set; }
        public Nullable<double> final { get; set; }
        public Nullable<System.DateTime> tim { get; set; }
        public Nullable<int> emp_id { get; set; }
        public Nullable<double> tax { get; set; }
        public Nullable<int> shift_id { get; set; }
        public int stk_id { get; set; }
        public Nullable<System.DateTime> due_date { get; set; }
        public Nullable<double> serv { get; set; }
        public double visa_amount { get; set; }
        public int cashtype { get; set; }
        public bool isConfirmed { get; set; }
        public Nullable<int> bnk_acc_id { get; set; }
        public int brch_id { get; set; }
    
        public virtual Banks_accounts Banks_accounts { get; set; }
        public virtual Branch Branch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_det_pack> sales_det_pack { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_det> sales_det { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual user user { get; set; }
    }
}
