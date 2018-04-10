using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        //[Display(Name = "Payment Via")]
        //public string PaymentVia { get; set; }

        public int FanID { get; set; }
        public virtual Fan Fan { get; set; }

      // public int OrderID { get; set; }
       public virtual Order Order { get; set; }
    }
}