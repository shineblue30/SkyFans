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
    public class FanPartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FanParts
        public ActionResult Index()
        {
            var fanParts = db.FanParts.Include(f => f.Fan).Include(f => f.VendorProduct);
            return View(fanParts.ToList());
        }

        // GET: FanParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FanPart fanPart = db.FanParts.Find(id);
            if (fanPart == null)
            {
                return HttpNotFound();
            }
            return View(fanPart);
        }





        public JsonResult GetPrice(int VendorProductID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Purchase> PriceList = db.Purchases.Where(x => x.VendorProductID == VendorProductID).ToList();
            var price = PriceList.Select(x => x.Price).Max();


            
            return Json(price, JsonRequestBehavior.AllowGet);

        }






        // GET: FanParts/Create
        public ActionResult Create()
        {
            ViewBag.FanID = new SelectList(db.Fans, "ID", "Name");
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            return View();
        }

        // POST: FanParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Qty,Price,VendorProductID,FanID,Total")] FanPart fanPart)
        {
            if (ModelState.IsValid)
            {
                db.FanParts.Add(fanPart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FanID = new SelectList(db.Fans, "ID", "Name", fanPart.FanID);
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", fanPart.VendorProductID);
            return View(fanPart);
        }

        // GET: FanParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FanPart fanPart = db.FanParts.Find(id);
            if (fanPart == null)
            {
                return HttpNotFound();
            }
            ViewBag.FanID = new SelectList(db.Fans, "ID", "Name", fanPart.FanID);
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", fanPart.VendorProductID);
            return View(fanPart);
        }

        // POST: FanParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Qty,Price,VendorProductID,FanID,Total")] FanPart fanPart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fanPart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FanID = new SelectList(db.Fans, "ID", "Name", fanPart.FanID);
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name", fanPart.VendorProductID);
            return View(fanPart);
        }

        // GET: FanParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FanPart fanPart = db.FanParts.Find(id);
            if (fanPart == null)
            {
                return HttpNotFound();
            }
            return View(fanPart);
        }

        // POST: FanParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FanPart fanPart = db.FanParts.Find(id);
            db.FanParts.Remove(fanPart);
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
