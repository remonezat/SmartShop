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
    
    public partial class items_units_lnks
    {
        public int id { get; set; }
        public int item_id { get; set; }
        public int sunit_id { get; set; }
        public double sunit_count { get; set; }
        public double sunit_sel { get; set; }
        public double sunit_pur { get; set; }
        public Nullable<double> min_price { get; set; }
        public bool compund_isBigest { get; set; }
    
        public virtual item item { get; set; }
        public virtual items_units items_units { get; set; }
    }
}
