using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkyFan.Models;

namespace SkyFan.Controllers
{
    public class FansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fans
        public ActionResult Index()
        {
            var fans = db.Fans;
            return View(fans.ToList());
        }

        // GET: Fans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);

            ViewBag.check = db.FanParts.Where(x => x.FanID == id).ToList();

            var total = db.FanParts.Where(x => x.FanID == id);

            if (total  != null)
            {
                var test = total.Sum(x => x.Total);
                ViewBag.sum = test;
            }
            else
            {
                ViewBag.sum = 0;
            }
           
                
           
           


            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: Fans/Create
        public ActionResult Create()
        {
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            return View();
        }

        // POST: Fans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Size,Picture,VendorProductID")] Fan fan, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {

                // File Uploads Logic
                if (Picture != null && Picture.ContentLength > 0)

                {

                    var savedFileName = DateTime.Now.Millisecond.ToString() + Path.GetFileName(Picture.FileName);
                    string path = Path.Combine(Server.MapPath("~/Attachments"), savedFileName);
                    fan.Picture = savedFileName;
                    Picture.SaveAs(path);
                    ViewBag.Notify = "File uploaded successfully";

                }


                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            return View(fan);
        }

        // GET: Fans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            Session["Picture"] = fan.Picture;
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            return View(fan);
        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Size,Picture,VendorProductID")] Fan fan, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {

                if (Picture != null && Picture.ContentLength > 0)

                {
                    if (!String.IsNullOrEmpty(fan.Picture))
                    {
                        string fullPath = Request.MapPath("~/Attachments/" + fan.Picture);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }


                    }




                    var savedFileName = DateTime.Now.Millisecond.ToString() + Path.GetFileName(Picture.FileName);
                    string path = Path.Combine(Server.MapPath("~/Attachments"), savedFileName);
                    fan.Picture = savedFileName;
                    Picture.SaveAs(path);
                    //ViewBag.Notify = "File uploaded successfully";

                }
                else if (Session["Picture"] != null)
                {

                    fan.Picture = Session["Picture"].ToString();

                }



                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VendorProductID = new SelectList(db.VendorProducts, "ID", "Name");
            return View(fan);
        }

        // GET: Fans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
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
