using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class StockOut
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int RemQuantity { get; set; }

        public int VendorID { get; set; }
        public int VendorProductID { get; set; }
        public int EmployeeID { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual VendorProduct VendorProduct { get; set; }
        public virtual Employee Employee { get; set; }
        
       

       

    }
}