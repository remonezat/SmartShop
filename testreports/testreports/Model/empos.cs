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
    
    public partial class empos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public empos()
        {
            this.Banks_acc_deposits = new HashSet<Banks_acc_deposits>();
            this.Banks_acc_transactions = new HashSet<Banks_acc_transactions>();
            this.Banks_acc_withdraws = new HashSet<Banks_acc_withdraws>();
            this.empos_attend = new HashSet<empos_attend>();
            this.empos_upDown = new HashSet<empos_upDown>();
            this.repre_comm_levels = new HashSet<repre_comm_levels>();
            this.representatives = new HashSet<representative>();
            this.accounts = new HashSet<account>();
        }
    
        public int id { get; set; }
        public string ename { get; set; }
        public string ephone { get; set; }
        public string eaddress { get; set; }
        public string eidnumber { get; set; }
        public Nullable<double> esalary { get; set; }
        public string job { get; set; }
        public string note { get; set; }
        public Nullable<System.TimeSpan> goin { get; set; }
        public Nullable<System.TimeSpan> goout { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<double> min_pri { get; set; }
        public string sa_typ { get; set; }
        public Nullable<int> id_device { get; set; }
        public Nullable<int> linked_userId { get; set; }
        public bool stopwork { get; set; }
        public string type { get; set; }
        public int dailyHours { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banks_acc_deposits> Banks_acc_deposits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banks_acc_transactions> Banks_acc_transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banks_acc_withdraws> Banks_acc_withdraws { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empos_attend> empos_attend { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empos_upDown> empos_upDown { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<repre_comm_levels> repre_comm_levels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<representative> representatives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<account> accounts { get; set; }
    }
}
