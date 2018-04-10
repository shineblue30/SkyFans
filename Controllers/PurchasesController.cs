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
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Purchases
        public ViewResult Index(string searchString,  DateTime? d1, DateTime? d2)
        {
            var purchases = db.Purchases.Include(p => p.VendorProduct);

            if(d1.HasValue && d2.HasValue)
            {
                
                
                purchases = purchases.Where(d => d.Date >= d1 && d.Date <= d2).OrderBy(c=>c.Date);

                if (purchases.Count() != 0)
                {
                    var sum2 = purchases.Sum(x => x.Balance);


                    ViewBag.balance = sum2;
                }
                
            }
           

            else if (!String.IsNullOrEmpty(searchString))
            {
                 purchases = purchases.Where(s => s.VendorProduct.Name.Contains(searchString) || s.VendorProduct.Vendor.Name.Contains(searchString));
                if (purchases.Count() != 0)
                {
                    var sum2 = purchases.Sum(x => x.Balance);


                    ViewBag.balance = sum2;
                }
            }

            else { 
                var sum = db.Purchases.Sum(x => x.Balance);


                ViewBag.balance = sum;

            }

            return View(purchases.ToList().OrderByDescending(c => c.Date));
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);

            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }



        public JsonResult GetVProductList(int VendorID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<VendorProduct> VendorList = db.VendorProducts.Where(x => x.VendorID == VendorID).ToList();
            return Json(VendorList, JsonRequestBehavior.AllowGet);

        }

        




        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name");
           

            VendorProduct vendorproduct = new VendorProduct();
           // ViewBag.Quantity = vendorproduct.Quantity;

            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Quantity,Price,Total,PaymentVia,AmountToPay,VendorProductID,VendorID, Balance")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {


                var amountToPay = purchase.Total - purchase.AmountToPay;

                purchase.Balance = amountToPay;


                Vendor vendor = new Vendor();

                vendor.Balance = purchase.Balance;
               // db.Vendors.Add(vendor);
                db.SaveChanges();



                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            
           

            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", purchase.VendorProductID);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", purchase.VendorID);
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", purchase.VendorProductID);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Quantity,Price,Total,PaymentVia,AmountToPay,VendorProductID,VendorID, Balance")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                var amountToPay = purchase.Total - purchase.AmountToPay;

                purchase.Balance = amountToPay;

                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", purchase.VendorProductID);
            //ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name");
            return View(purchase);
        }


        



        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult Deleted(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
            db.SaveChanges();
            return Json("Index");
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
