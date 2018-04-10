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
    public class VendorProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VendorProducts
        public ActionResult Index()
        {
            var vendorProducts = db.VendorProducts.Include(v => v.Vendor);
            return View(vendorProducts.ToList());
        }

        // GET: VendorProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorProduct vendorProduct = db.VendorProducts.Find(id);
            if (vendorProduct == null)
            {
                return HttpNotFound();
            }
            return View(vendorProduct);
        }

        // GET: VendorProducts/Create
        public ActionResult Create()
        {
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name");
            return View();
        }

        // POST: VendorProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,VendorID")] VendorProduct vendorProduct)
        {
            if (ModelState.IsValid)
            {
                db.VendorProducts.Add(vendorProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", vendorProduct.VendorID);
            return View(vendorProduct);
        }

        // GET: VendorProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorProduct vendorProduct = db.VendorProducts.Find(id);
            if (vendorProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", vendorProduct.VendorID);
            return View(vendorProduct);
        }

        // POST: VendorProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,VendorID")] VendorProduct vendorProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendorProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", vendorProduct.VendorID);
            return View(vendorProduct);
        }

        // GET: VendorProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorProduct vendorProduct = db.VendorProducts.Find(id);
            if (vendorProduct == null)
            {
                return HttpNotFound();
            }
            return View(vendorProduct);
        }

        // POST: VendorProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendorProduct vendorProduct = db.VendorProducts.Find(id);
            db.VendorProducts.Remove(vendorProduct);
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
