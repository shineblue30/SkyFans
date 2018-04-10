using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyFan.Models
{
    public class Dealer
    {
        public int ID { get; set; }

        [Display(Name = "Dealer Name")]
        [Remote("CheckUserNameExists", "Dealers", ErrorMessage = "Dealer Name already exists!")]
        public String Name { get; set; }
        public String Address { get; set; }
        public int PhoneNumber { get; set; }
        public int Balance { get; set; }

        //public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<DealerProduct> DealerProduct { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}