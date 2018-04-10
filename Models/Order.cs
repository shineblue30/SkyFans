using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class Order
    {

        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            
        }

        public int ID { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        public int DealerID { get; set; }
        //public int OrderDetailID { get; set; }



        public virtual Dealer Dealer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        
    }
} 