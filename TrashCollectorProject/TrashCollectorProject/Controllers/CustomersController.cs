﻿using Microsoft.AspNet.Identity;
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

        [Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            string currentId = User.Identity.GetUserId();
            Customer customer = db.Customers.FirstOrDefault(x => x.UserId == currentId);

            return View(customer);
        }

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

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Edit()
        {
            string currentId = User.Identity.GetUserId();
            Customer customer = db.Customers.FirstOrDefault(x => x.UserId == currentId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HoldPickup(Customer customer)
        {
            string todaysDateString = DateTime.Now.ToString("MM/dd/yyyy");
            var todaysDateVar = DateTime.Parse(todaysDateString);
            var holdStart = DateTime.Parse(customer.holdPickUpStart);
            var holdEnd = DateTime.Parse(customer.holdPickUpEnd);
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                if (todaysDateVar > holdStart && todaysDateVar < holdEnd)
                {
                    customer.onHold = true;
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(customer);
        }
        
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
