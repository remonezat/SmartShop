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
    
    public partial class payment
    {
        public int id { get; set; }
        public Nullable<int> acc_id { get; set; }
        public Nullable<double> amount_cash { get; set; }
        public Nullable<double> amount_cobon { get; set; }
        public Nullable<bool> input { get; set; }
        public Nullable<int> date { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> tim { get; set; }
        public Nullable<int> shift_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public bool isConfirmed { get; set; }
        public int bnk_acc_id { get; set; }
        public int brch_id { get; set; }
    
        public virtual Banks_accounts Banks_accounts { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual user user { get; set; }
    }
}
