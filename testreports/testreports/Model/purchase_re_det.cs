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
    
    public partial class purchase_re_det
    {
        public int inv_id { get; set; }
        public Nullable<int> date { get; set; }
        public int item_id { get; set; }
        public double price { get; set; }
        public double count { get; set; }
        public int u_id { get; set; }
        public double u_count { get; set; }
        public int id { get; set; }
    
        public virtual item item { get; set; }
        public virtual items_units items_units { get; set; }
        public virtual purchase purchase { get; set; }
    }
}
