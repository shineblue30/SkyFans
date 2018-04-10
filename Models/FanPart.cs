using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class FanPart
    {
        public int ID { get; set; }
        [Display(Name = "Quantity")]
        public int Qty { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public int VendorProductID { get; set; }
       
        public int FanID { get; set; }

        public virtual VendorProduct VendorProduct { get; set; }
       
        public virtual Fan  Fan { get; set; }
    }
}