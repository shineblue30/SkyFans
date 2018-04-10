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
    public class StockOutsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StockOuts
        public ActionResult Index(string searchString, DateTime? d1, DateTime? d2)
        {
            var stockOut = db.StockOut.Include(s => s.Employee);


            if (d1.HasValue && d2.HasValue)
            {


                stockOut = stockOut.Where(d => d.Date >= d1 && d.Date <= d2).OrderBy(c => c.Date);

                //if (stockOut.Count() != 0)
                //{
                //    var sum2 = stockOut.Sum(x => x.Balance);


                //    ViewBag.balance = sum2;
                //}

            }


            else if (!String.IsNullOrEmpty(searchString))
            {
                stockOut = stockOut.Where(s => s.VendorProduct.Name.Contains(searchString) || s.VendorProduct.Vendor.Name.Contains(searchString));
                //if (stockOut.Count() != 0)
                //{
                //    var sum2 = stockOut.Sum(x => x.Balance);


                //    ViewBag.balance = sum2;
                //}
            }

            //else
            //{
            //    var sum = db.Purchases.Sum(x => x.Balance);


            //    ViewBag.balance = sum;

            //}



            return View(stockOut.ToList().OrderBy(c => c.Date));
        }

        // GET: StockOuts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOut stockOut = db.StockOut.Find(id);
            if (stockOut == null)
            {
                return HttpNotFound();
            }
            return View(stockOut);
        }


        public JsonResult GetVProductList(int VendorID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<VendorProduct> VendorList = db.VendorProducts.Where(x => x.VendorID == VendorID).ToList();
            return Json(VendorList, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetQuantityList(int VendorProductID, int VendorID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Purchase> CompanyList = db.Purchases.Where(x => x.VendorProductID == VendorProductID && x.VendorID == VendorID).ToList();
            var Pquantity = CompanyList.Sum(x => x.Quantity);


            List<StockOut> StockList = db.StockOut.Where(x => x.VendorProductID == VendorProductID && x.VendorID == VendorID).ToList();
            var Squantity = StockList.Sum(x => x.Quantity);

            var quantity = Pquantity - Squantity  ;
            return Json(quantity, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult PostQuantity(int Quantity, int VendorID, int VendorProductID)
        {
            List<Purchase> CompanyList = db.Purchases.Where(y => y.VendorID == VendorID && y.VendorProductID == VendorProductID).ToList();
           
            var quantity =  CompanyList.Sum(x => x.Quantity);

            var test = quantity - Quantity;
            
            //db.SaveChanges();
            return Json(quantity, JsonRequestBehavior.AllowGet);
        }


       

        // GET: StockOuts/Create
        public ActionResult Create()
        {
            

            ViewBag.EmployeeID = new SelectList(db.Emloyee, "ID", "Name");
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name");
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            return View();
        }

        // POST: StockOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Quantity,EmployeeID,VendorProductID,VendorID,RemQuantity")] StockOut stockOut)
        {
            if (ModelState.IsValid)
            {
                db.StockOut.Add(stockOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Emloyee, "ID", "Name", stockOut.EmployeeID);
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name",stockOut.VendorProductID);
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", stockOut.VendorID);
            return View(stockOut);
        }

        // GET: StockOuts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOut stockOut = db.StockOut.Find(id);
            if (stockOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Emloyee, "ID", "Name", stockOut.EmployeeID);
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", stockOut.VendorProductID);
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", stockOut.VendorID);
            return View(stockOut);
        }

        // POST: StockOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Quantity,EmployeeID,VendorProductID,VendorID,RemQuantity")] StockOut stockOut)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(stockOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Emloyee, "ID", "Name", stockOut.EmployeeID);
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", stockOut.VendorProductID);
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", stockOut.VendorID);
            return View(stockOut);
        }

        // GET: StockOuts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOut stockOut = db.StockOut.Find(id);
            if (stockOut == null)
            {
                return HttpNotFound();
            }
            return View(stockOut);
        }

        // POST: StockOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockOut stockOut = db.StockOut.Find(id);
            db.StockOut.Remove(stockOut);
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
