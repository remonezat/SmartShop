using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.PublicClasses
{
    public class BillDetails
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public float Total { get; set; }
        public float FinalTotal { get; set; }
        public int ItemsCount { get; set; }
        public float TotalQuantity { get; set; }



    }
}