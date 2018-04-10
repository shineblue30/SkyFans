using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyFan.Models
{
    public class Vendor
    {
        public int ID { get; set; }

        [Display(Name="Vendor Name")]
        [Remote("CheckUserNameExists", "Vendors", ErrorMessage = "Vendor Name already exists!")]
        public String Name { get; set; }
        public String Address { get; set; }
        public int PhoneNumber { get; set; }
        public int Balance { get; set; }

        //public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<VendorProduct> VendorProduct { get; set; }
      //  public virtual ICollection<StockOut> StockOut { get; set; }


    }
}

