using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }
       
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public int AmountToPay { get; set; }
        public int Balance { get; set; }
        public String PaymentVia { get; set; }

       
        public int VendorProductID { get; set; }

        [Display(Name ="Vendor Name")]
        public int VendorID { get; set; }
        public virtual VendorProduct VendorProduct { get; set; }
        //public virtual Vendor Vendor { get; set; }

    }
}

