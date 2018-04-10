using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class DealerProduct
    {
        public int ID { get; set; }

        [Display(Name = "Product Name")]
        public String Name { get; set; }



        public int DealerID { get; set; }

        public virtual Dealer Dealer { get; set; }

        public virtual ICollection<Sale> Sale { get; set; }
    }
}