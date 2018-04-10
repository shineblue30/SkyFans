using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SkyFan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyFan.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

           

            var sum = db.Purchases.Sum(x => x.Balance);
           
            if (sum != 0) { 

                ViewBag.balance = sum;

            }
            else
            {
                ViewBag.balance = 0;
            }


            var vl = db.Vendors.OrderByDescending(x => x.Balance).ToList();

            if(vl != null)
            {
                ViewBag.vendorlist = vl;
            }
            else
            {
                ViewBag.vendorlist = 0;
            }
            



            var ds = db.Sale.Where(x => x.Date == DateTime.Today);
            
            if(ds.Any())
            {
                ViewBag.dailySale = ds.Sum(y => y.Quantity);
            }
            else
            {
                ViewBag.dailySale = 0;
            }


            var dp = db.Purchases.Where(x => x.Date == DateTime.Today);
            if(dp.Any())
            { 
            ViewBag.dailyPurchase = dp.Sum(y => y.Quantity);

            }
            else
            {
                ViewBag.dailyPurchase = 0;
            }


            var sum1 = db.Sale.Sum(x => x.Balance);

            if (sum1 != 0)
            {
                
                ViewBag.balance1 = sum1;
            }
            else
            {
                ViewBag.balance1 = 0;
            }




           var dl  = db.Dealer.OrderByDescending(x => x.Balance).ToList();

            if(dl != null)
            {
                ViewBag.dealerlist = dl;
            }
            else
            {
                ViewBag.dealerlist = 0;
            }


            ViewBag.SaleDates = db.Sale.OrderBy(y=>y.Date).ToList();




          


            return View();
        }

        public ContentResult GetDataDealer()
        {
            List<Dealer> Home = new List<Dealer>();
            var result = db.Dealer.ToList();

            foreach(Dealer dealer in result)
            {
                Dealer dealerM = new Dealer();
                dealerM.Name = dealer.Name;
                dealerM.Balance = dealer.Balance;
                Home.Add(dealerM);
            }

            return Content(JsonConvert.SerializeObject(Home), "application/json");
            
        }




        public ContentResult GetDataVendor()
        {
            List<Vendor> Home1 = new List<Vendor>();
            var result = db.Vendors.ToList();

            foreach (Vendor vendor in result)
            {
                Vendor vendorM = new Vendor();
                vendorM.Name = vendor.Name;
                vendorM.Balance = vendor.Balance;
                Home1.Add(vendorM);
            }

            return Content(JsonConvert.SerializeObject(Home1), "application/json");

        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}