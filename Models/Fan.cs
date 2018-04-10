using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyFan.Models
{
    public class Fan
    {
        public int ID { get; set; }
        [Display(Name="Model Name")]
        public string Name { get; set; }
        public decimal Size { get; set; }
        public string Picture { get; set; }


        public virtual ICollection<FanPart> FanPart { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}