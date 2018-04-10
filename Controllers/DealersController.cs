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
    public class DealersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dealers
        public ActionResult Index()
        {
            return View(db.Dealer.ToList());
        }

        // GET: Dealers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealer.Find(id);


            dealer.DealerProduct.Select(p => p.Name);


            var y = db.Sale.Where(x => x.DealerID == id).ToList();

            ViewBag.check = y;
            ViewBag.bal = y.Sum(z => z.Balance);

            dealer.Balance = ViewBag.bal;
            db.SaveChanges();











            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }



        public ActionResult CheckUserNameExists(string Name)
        {
            bool UserExists = false;

            try
            {
                using (var dbcontext = new ApplicationDbContext())
                {
                    var nameexits = db.Dealer.Where(x => x.Name == Name);
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



        // GET: Dealers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dealers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,PhoneNumber,Balance")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                db.Dealer.Add(dealer);
                db.SaveChanges();
                ViewBag.AlertMessage = "my alert message";
                return RedirectToAction("Index");
            }

            return View(dealer);
        }

        // GET: Dealers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealer.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }

        // POST: Dealers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,PhoneNumber,Balance")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dealer);
        }

        // GET: Dealers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealer.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }

        // POST: Dealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dealer dealer = db.Dealer.Find(id);
            db.Dealer.Remove(dealer);
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
