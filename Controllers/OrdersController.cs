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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Dealer).Include(o=>o.OrderDetails);
            return View(orders.ToList());
        }


        //Post action for Save data to database
        [HttpPost]
        public JsonResult SaveOrder(Order O)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
               
                    Order order = new Order {  OrderDate = O.OrderDate, DeliveryDate = O.DeliveryDate, DealerID = O.DealerID };
                foreach (var i in O.OrderDetails)
                {

                   
                        order.OrderDetails.Add(i);
                }
                    db.Orders.Add(order);
                    db.SaveChanges();
                    status = true;
                
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }





        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);



            var y = db.OrderDetails.Where(x => x.Order.ID == id).ToList();

            ViewBag.check = y;
            ViewBag.Total = y.Sum(z => z.Total);

           





            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.DealerID = new SelectList(db.Dealer, "ID", "Name");
            ViewBag.FanID = new SelectList(db.Fans, "ID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderDate,DeliveryDate,DealerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DealerID = new SelectList(db.Dealer, "ID", "Name", order.Dealer.ID);
            ViewBag.OrderDetailID = new SelectList(db.OrderDetails, "ID", "PaymentVia");
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.DealerID = new SelectList(db.Dealer, "ID", "Name");
            ViewBag.OrderDetailID = new SelectList(db.OrderDetails, "ID", "PaymentVia");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderDate,DeliveryDate,DealerID,OrderDetailID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealerID = new SelectList(db.Dealer, "ID", "Name");
            ViewBag.OrderDetailID = new SelectList(db.OrderDetails, "ID", "PaymentVia");
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
