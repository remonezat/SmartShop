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
    
    public partial class installment_pays
    {
        public int id { get; set; }
        public System.DateTime duedate { get; set; }
        public Nullable<double> due_amount { get; set; }
        public Nullable<double> paid_amount { get; set; }
        public Nullable<int> install_id { get; set; }
        public Nullable<System.DateTime> paydate { get; set; }
    }
}
