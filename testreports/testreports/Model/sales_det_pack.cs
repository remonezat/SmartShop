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
    
    public partial class sales_det_pack
    {
        public int inv_id { get; set; }
        public int mitem_id { get; set; }
        public int item_id { get; set; }
        public double count { get; set; }
    
        public virtual item item { get; set; }
        public virtual item item1 { get; set; }
        public virtual sale sale { get; set; }
    }
}
