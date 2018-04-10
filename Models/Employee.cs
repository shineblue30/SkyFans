using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyFan.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Display(Name = "Employee Name")]
        [Remote("CheckUserNameExists", "Employees", ErrorMessage = "Employee Name already exists!")]
        public String Name { get; set; }

        public virtual ICollection<StockOut> StockOut { get; set; }
    }
}