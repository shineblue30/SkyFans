using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkyFan.Models;

namespace SkyFan.Controllers
{
    [Authorize]
    public class VendorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vendors
        public ActionResult Index()
        {

            var sum = db.Purchases.Sum(x => x.Balance);


            ViewBag.balance = sum;


            return View(db.Vendors.ToList());
        }




        public ActionResult CheckUserNameExists(string Name)
        {
            bool UserExists = false;

            try
            {
                using (var dbcontext = new ApplicationDbContext())
                {
                    var nameexits = db.Vendors.Where(x => x.Name == Name);
                    if (nameexits.Count() > 0)
                    {
                        UserExists = true;
                    }
                    else
                    {
                        UserExists = false;
                    }
                }
                return Json(!UserExists, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }




        // GET: Vendors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Purchase purchase = new Purchase();
            

            Vendor vendor = db.Vendors.Find(id);

            vendor.VendorProduct.Select(p => p.Name);
          

            var y = db.Purchases.Where(x => x.VendorID == id ).ToList();

            ViewBag.check = y;
            ViewBag.bal = y.Sum(z=>z.Balance);
            
            vendor.Balance = ViewBag.bal;
            db.SaveChanges();

          





            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }



        //public JsonResult GetAllVendors(int? id)
        //{
           
        //    var purchase = db.Purchases.Where(x =>x.ID==1
        //    //{
        //    //    Id = x.ID,
        //    //    Name = x.VendorID,
        //    //    Product = x.VendorProductID,
        //    //    Date = x.Date,
        //    //    Quantity = x.Quantity,
        //    //    Price= x.Price,
        //    //    Tottal= x.Total,
        //    //    AmountToPay=x.AmountToPay,
        //    //    Balance=x.Balance,
        //    //    PaymentVia=x.PaymentVia
        //    //}
        //    ).ToList();
        //    return Json(purchase, JsonRequestBehavior.AllowGet);
        //}





        // GET: Vendors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,PhoneNumber,Balance")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);


            
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,PhoneNumber,Balance")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor).State = EntityState.Modified;


               



                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
