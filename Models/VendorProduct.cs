using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class VendorProduct
    {
        public int ID { get; set; }

        [Display(Name = "Product Name")]
        public String Name { get; set; }
       


        public int VendorID { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
        public virtual ICollection<FanPart> FanPart { get; set; }
        // public virtual ICollection<StockOut> StockOut { get; set; }
    }
}


