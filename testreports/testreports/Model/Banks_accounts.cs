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
    
    public partial class Banks_accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Banks_accounts()
        {
            this.Banks_acc_deposits = new HashSet<Banks_acc_deposits>();
            this.Banks_acc_transactions = new HashSet<Banks_acc_transactions>();
            this.Banks_acc_transactions1 = new HashSet<Banks_acc_transactions>();
            this.Banks_acc_withdraws = new HashSet<Banks_acc_withdraws>();
            this.Branches = new HashSet<Branch>();
            this.empos_upDown = new HashSet<empos_upDown>();
            this.Incomes = new HashSet<Income>();
            this.Outcomes1 = new HashSet<Outcome>();
            this.payments = new HashSet<payment>();
            this.purchases1 = new HashSet<purchase>();
            this.purchase_re = new HashSet<purchase_re>();
            this.sales = new HashSet<sale>();
            this.sales_re = new HashSet<sales_re>();
        }
    
        public int id { get; set; }
        public string bnk_acc_name { get; set; }
        public double first_balance { get; set; }
        public bool closeShift { get; set; }
        public bool OutCOmes { get; set; }
        public bool PayToSupp { get; set; }
        public bool recFromCust { get; set; }
        public bool purchases { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banks_acc_deposits> Banks_acc_deposits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banks_acc_transactions> Banks_acc_transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banks_acc_transactions> Banks_acc_transactions1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banks_acc_withdraws> Banks_acc_withdraws { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Branch> Branches { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empos_upDown> empos_upDown { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Income> Incomes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Outcome> Outcomes1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<payment> payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<purchase> purchases1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<purchase_re> purchase_re { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sale> sales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sales_re> sales_re { get; set; }
    }
}
