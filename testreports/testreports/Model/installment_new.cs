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
    
    public partial class installment_new
    {
        public int id { get; set; }
        public string cust_name { get; set; }
        public string cust_id { get; set; }
        public string cust_phone { get; set; }
        public string cust_address { get; set; }
        public string guar_name { get; set; }
        public string guar_id { get; set; }
        public string guar_phone { get; set; }
        public string guar_address { get; set; }
        public string item_name { get; set; }
        public string item_lic { get; set; }
        public string item_chassis { get; set; }
        public string slr_name { get; set; }
        public string slr_id { get; set; }
        public string slr_phone { get; set; }
        public string slr_address { get; set; }
        public double price_pur { get; set; }
        public double tot { get; set; }
        public Nullable<double> pre { get; set; }
        public Nullable<double> pre_remain { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<System.DateTime> reg_date { get; set; }
        public Nullable<double> discount { get; set; }
        public double prfts { get; set; }
    }
}