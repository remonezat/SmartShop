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
    
    public partial class empos_pay
    {
        public int id { get; set; }
        public Nullable<int> emp_id { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<int> date { get; set; }
        public string note { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<System.DateTime> tim { get; set; }
        public Nullable<int> shift_id { get; set; }
    }
}
