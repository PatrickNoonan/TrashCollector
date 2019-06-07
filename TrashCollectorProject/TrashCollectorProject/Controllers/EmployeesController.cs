using Microsoft.AspNet.Identity;
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

        // GET: Employees
        public ActionResult Index(string weekDayString)
        {
            List<SelectListItem> weekDays = new List<SelectListItem>();
            SelectListItem Sunday = new SelectListItem() { Text = "Sunday", Value = "1", Selected = true };
            SelectListItem Monday = new SelectListItem() { Text = "Monday", Value = "2", Selected = true };
            SelectListItem Tuesday = new SelectListItem() { Text = "Tuesday", Value = "3", Selected = true };
            SelectListItem Wednesday = new SelectListItem() { Text = "Wednesday", Value = "4", Selected = true };
            SelectListItem Thursday = new SelectListItem() { Text = "Thursday", Value = "5", Selected = true };
            SelectListItem Friday = new SelectListItem() { Text = "Friday", Value = "6", Selected = true };
            SelectListItem Saturday = new SelectListItem() { Text = "Saturday", Value = "7", Selected = true };
            weekDays.Add(Sunday); weekDays.Add(Monday); weekDays.Add(Tuesday); weekDays.Add(Wednesday); weekDays.Add(Thursday); weekDays.Add(Friday); weekDays.Add(Saturday);

            if (weekDayString != null)
            {
                weekDays.Where(i => i.Value == weekDayString).First().Selected = true;
            }

            ViewBag.DaysOfWeek = weekDays;

            
            //var players = _context.Players.Include(m => m.Team).ToList();
            string today = System.DateTime.Now.DayOfWeek.ToString();
            string currentId = User.Identity.GetUserId();
            Employee employee = db.Employees.FirstOrDefault(x => x.UserId == currentId);

            var customers = db.Customers.Where(c => c.zipCode == employee.zipCode && c.weeklyPickupDay == today ).ToList();

            return View(customers);
        }

        // GET: Employees/Edit/5
        //public ActionResult FilterByDay()
        //{
        //    string today = "weekDay";
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(today);
        //}

        //// POST: Employees/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult FilterByDay(string weekday)
        //{
        //    //var players = _context.Players.Include(m => m.Team).ToList();

        //    string currentId = User.Identity.GetUserId();
        //    Employee employee = db.Employees.FirstOrDefault(x => x.UserId == currentId);

        //    var customers = db.Customers.Where(c => c.zipCode == employee.zipCode && c.weeklyPickupDay == today).ToList();

        //    return View(customers);
        //}

        // GET: /Details/5
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

        // GET: Employees/Edit/5
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

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Employees/Delete/5
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

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET:
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

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPickup( Customer customer)
        //[Bind(Include = "Id,firstName,lastName,emailAddress,address,zipcode,weeklyPickupDay,specialOneTimePickup")]
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
