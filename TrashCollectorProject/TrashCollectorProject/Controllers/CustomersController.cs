using Microsoft.AspNet.Identity;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollectorProject.Models;

namespace TrashCollectorProject.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            string currentId = User.Identity.GetUserId();

            //Customer customer = db.Customers.Find(currentId);
            Customer customer = db.Customers.FirstOrDefault(x => x.UserId == currentId);

            return View(customer);
        }

        // GET: Customers/Details/5
        public ActionResult Details()
        {
            string currentId = User.Identity.GetUserId();

            Customer customer = db.Customers.FirstOrDefault(x => x.UserId == currentId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                string currentId = User.Identity.GetUserId();

                db.Customers.Add(customer);
                customer.UserId = currentId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit()
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            string currentId = User.Identity.GetUserId();

            Customer customer = db.Customers.FirstOrDefault(x => x.UserId == currentId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,firstName,lastName,emailAddress,address,zipCode,weeklyPickupDay,specialOneTimePickup")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                string currentId = User.Identity.GetUserId();
                customer.UserId = currentId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete()
        {
            string currentId = User.Identity.GetUserId();

            Customer customer = db.Customers.FirstOrDefault(x => x.UserId == currentId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // GET: Customers/HoldPickup/5
        public ActionResult HoldPickup()
        {            
            string currentId = User.Identity.GetUserId();
            Customer customer = db.Customers.SingleOrDefault(m => m.UserId == currentId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HoldPickup(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        // GET: Customers/SpecialPickup/5
        public ActionResult SpecialPickup()
        {
            string currentId = User.Identity.GetUserId();
            Customer customer = db.Customers.SingleOrDefault(m => m.UserId == currentId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/SpecialPickup/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SpecialPickup(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        // GET: Customers/Budget/5
        public ActionResult Budget()
        {
            string currentId = User.Identity.GetUserId();

            Customer customer = db.Customers.FirstOrDefault(x => x.UserId == currentId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
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
