using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class Sale
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


        public int DealerProductID { get; set; }

        [Display(Name = "Dealer Name")]
        public int DealerID { get; set; }
        public virtual DealerProduct DealerProduct { get; set; }
        //public virtual Vendor Vendor { get; set; }
    }
}