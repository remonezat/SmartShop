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
    
    public partial class order
    {
        public int id { get; set; }
        public string shop_no { get; set; }
        public string item_no { get; set; }
        public string description { get; set; }
        public Nullable<double> ctns { get; set; }
        public Nullable<double> qctns { get; set; }
        public string unit { get; set; }
        public Nullable<double> tt_qyt { get; set; }
        public Nullable<double> u_pri { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<double> ucbm { get; set; }
        public Nullable<double> tcbm { get; set; }
        public Nullable<double> uw { get; set; }
        public Nullable<double> tw { get; set; }
        public byte[] pic { get; set; }
        public string descr2 { get; set; }
        public Nullable<int> sers { get; set; }
    }
}