﻿using Microsoft.AspNet.Identity;
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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void ListOfDays()
        {
            //List<SelectListItem> weekDays = new List<SelectListItem>();
            //SelectListItem Today = new SelectListItem() { Text = "Today", Value = "0", Selected = true };
            //SelectListItem Sunday = new SelectListItem() { Text = "Sunday", Value = "1", Selected = true };
            //SelectListItem Monday = new SelectListItem() { Text = "Monday", Value = "2", Selected = true };
            //SelectListItem Tuesday = new SelectListItem() { Text = "Tuesday", Value = "3", Selected = true };
            //SelectListItem Wednesday = new SelectListItem() { Text = "Wednesday", Value = "4", Selected = true };
            //SelectListItem Thursday = new SelectListItem() { Text = "Thursday", Value = "5", Selected = true };
            //SelectListItem Friday = new SelectListItem() { Text = "Friday", Value = "6", Selected = true };
            //SelectListItem Saturday = new SelectListItem() { Text = "Saturday", Value = "7", Selected = true };
            //weekDays.Add(Today); weekDays.Add(Sunday); weekDays.Add(Monday); weekDays.Add(Tuesday); weekDays.Add(Wednesday); weekDays.Add(Thursday); weekDays.Add(Friday); weekDays.Add(Saturday);
                       
            List<string> weekDays = new List<string>();
            weekDays.Add("Monday");
            weekDays.Add("Tuesday");
            weekDays.Add("Wednesday");
            weekDays.Add("Thursday");
            weekDays.Add("Friday");
            weekDays.Add("Saturday");
            weekDays.Add("Sunday");

            //ViewBag.DaysOfWeek = weekDays;
        }
       
        public ActionResult Index(string dayOfWeek)
        {
            string today = System.DateTime.Now.DayOfWeek.ToString();

            if (dayOfWeek == null)
            {
                dayOfWeek = today;
            }
            
            string currentId = User.Identity.GetUserId();
            Employee employee = db.Employees.FirstOrDefault(x => x.UserId == currentId);

            var customers = db.Customers.Where(c => c.zipCode == employee.zipCode && c.weeklyPickupDay == dayOfWeek).ToList();

            return View(customers);
        }

        public ActionResult Details()
        {
            string currentId = User.Identity.GetUserId();

            Employee employee = db.Employees.FirstOrDefault(x => x.UserId == currentId);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                string currentId = User.Identity.GetUserId();

                db.Employees.Add(employee);
                employee.UserId = currentId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,firstName,lastName,emailAddress,address,zipcode,weeklyPickupDay,specialOneTimePickup")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ConfirmPickup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //string currentId = User.Identity.GetUserId();

            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPickup( Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                customer.weeklyPickupConfirmed = true;
                customer.Bill += 10;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public ActionResult GoogleMapsAPI(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult MapAllDailyStops(List<Customer> customerList)
        {
            //List<Customer> customerList = new List<Customer>();

            //foreach (int id in ids)
            //{
            //    Customer customer = db.Customers.Find(id);

            //    if (customer == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    customerList.Add(customer);
            //}
            int customerListLength = customerList.Count;
            int[] latsArray = new int[customerListLength];
            int[] longsArray = new int[customerListLength];

            for ( int i = 0; i < customerListLength; i++)
            {
                latsArray[i] = Convert.ToInt16(customerList[i].latitude);
                longsArray[i] = Convert.ToInt16(customerList[i].longitude);
            }

            ViewBag.latBag = latsArray;
            ViewBag.longBag = longsArray;
            ViewBag.bagLength = customerListLength;

            return View(customerList);
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
