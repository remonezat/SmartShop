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
    
    public partial class WorkOrder
    {
        public int id { get; set; }
        public int fctry_id { get; set; }
        public int emp_id { get; set; }
        public System.DateTime reg_date { get; set; }
        public Nullable<System.DateTime> open_date { get; set; }
        public Nullable<System.DateTime> close_date { get; set; }
        public int usr_id { get; set; }
        public bool status_isOpen { get; set; }
        public string notes { get; set; }
    }
}
