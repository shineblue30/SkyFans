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
    public class SalesController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sales
        public ActionResult Index(string searchString, DateTime? d1, DateTime? d2)
        {
            var sale = db.Sale.Include(s => s.DealerProduct);


                      
            if (d1.HasValue && d2.HasValue)
            {


                sale = sale.Where(d => d.Date >= d1 && d.Date <= d2).OrderBy(c => c.Date);

                if (sale.Count() != 0)
                {
                    var sum2 = sale.Sum(x => x.Balance);


                    ViewBag.balance = sum2;
                }

            }


            else if (!String.IsNullOrEmpty(searchString))
            {
                sale = sale.Where(s => s.DealerProduct.Name.Contains(searchString) || s.DealerProduct.Dealer.Name.Contains(searchString));
                if (sale.Count() != 0)
                {
                    var sum2 = sale.Sum(x => x.Balance);


                    ViewBag.balance = sum2;
                }
            }

            else
            {
                var sum = db.Sale.Sum(x => x.Balance);


                ViewBag.balance = sum;

            }




            return View(sale.ToList().OrderBy(c => c.Date));
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }




        public JsonResult GetSProductList(int DealerID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<DealerProduct> DealerList = db.DealerProducts.Where(x => x.DealerID == DealerID).ToList();
            return Json(DealerList, JsonRequestBehavior.AllowGet);

        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.DealerProductID = new SelectList(db.DealerProducts, "ID", "Name");
            ViewBag.DealerID = new SelectList(db.Dealer, "ID", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,Quantity,Price,Total,AmountToPay,Balance,PaymentVia,DealerProductID,DealerID")] Sale sale)
        {
            if (ModelState.IsValid)
            {

                var amountToPay = sale.Total - sale.AmountToPay;

                sale.Balance = amountToPay;
                db.SaveChanges();

                db.Sale.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DealerProductID = new SelectList(db.DealerProducts, "ID", "Name", sale.DealerProductID);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.DealerProductID = new SelectList(db.DealerProducts, "ID", "Name", sale.DealerProductID);
            ViewBag.DealerID = new SelectList(db.Dealer, "ID", "Name", sale.DealerID);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Quantity,Price,Total,AmountToPay,Balance,PaymentVia,DealerProductID,DealerID")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                var amountToPay = sale.Total - sale.AmountToPay;

                sale.Balance = amountToPay;

                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealerProductID = new SelectList(db.DealerProducts, "ID", "Name", sale.DealerProductID);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sale.Find(id);
            db.Sale.Remove(sale);
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
