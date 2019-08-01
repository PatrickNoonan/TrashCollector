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
        public string selectedDay = System.DateTime.Now.DayOfWeek.ToString();

        public void ListOfDays()
        {    
            List<string> weekDays = new List<string>();
            weekDays.Add("Monday");
            weekDays.Add("Tuesday");
            weekDays.Add("Wednesday");
            weekDays.Add("Thursday");
            weekDays.Add("Friday");
            weekDays.Add("Saturday");
            weekDays.Add("Sunday");

            ViewBag.WeekDays = weekDays;
        }

        public void FindDay(string dayOfWeek)
        {
            string today = System.DateTime.Now.DayOfWeek.ToString();
            selectedDay = dayOfWeek;
            if (dayOfWeek == null)
            {
                selectedDay = today;
            }
        }

        public Employee GetCurrentEmployee()
        {
            string currentId = User.Identity.GetUserId();
            Employee employee = db.Employees.FirstOrDefault(x => x.UserId == currentId);

            return employee;
        }

        public List<Customer> CustomersToDisplay(string dayOfWeek)
        {
            FindDay(dayOfWeek);
            Employee employee = GetCurrentEmployee();
            List<Customer> customers = db.Customers.Where(c => c.zipCode == employee.zipCode && c.weeklyPickupDay == selectedDay && c.weeklyPickupConfirmed == false && c.onHold == false ).ToList();
            List<Customer> customersToAdd = db.Customers.Where(c => c.zipCode == employee.zipCode && c.specialOneTimePickup == selectedDay && c.specialPickupConfirmed == false).ToList();
            foreach (Customer item in customersToAdd)
            {
                customers.Add(item);
            }
            return customers;
        }

        [Authorize(Roles = "Employee")]
        public ActionResult Index(string dayOfWeek)
        {
            List<Customer> customers = CustomersToDisplay(dayOfWeek);
            
            return View(customers);
        }

        public ActionResult Details()
        {
            Employee employee = GetCurrentEmployee();
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

        public ActionResult Edit()
        {
            string currentId = User.Identity.GetUserId();
            Employee employee = db.Employees.FirstOrDefault(x => x.UserId == currentId);
           
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "firstName,lastName,zipCode,city,state")] Employee employee)
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
                if (customer.weeklyPickupDay == selectedDay && customer.weeklyPickupConfirmed == false)
                {
                    customer.weeklyPickupConfirmed = true;
                    customer.Bill += 10;
                }
                else if (customer.specialOneTimePickup == selectedDay && customer.specialPickupConfirmed == false)
                {
                    customer.specialPickupConfirmed = true;
                    customer.Bill += 20;
                }
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

        public void coordsViewBagMaker(List<Customer> customerList)
        {
            int customerListLength = customerList.Count;
            int[] latsArray = new int[customerListLength];
            int[] longsArray = new int[customerListLength];

            for (int i = 0; i < customerListLength; i++)
            {
                latsArray[i] = Convert.ToInt16(customerList[i].latitude);
                longsArray[i] = Convert.ToInt16(customerList[i].longitude);
            }

            ViewBag.latBag = latsArray;
            ViewBag.longBag = longsArray;
            ViewBag.bagLength = customerListLength;
        }

        public ActionResult MapAllDailyStops()
        {
            Employee employee = GetCurrentEmployee();
            var customerList = db.Customers.Where(c => c.zipCode == employee.zipCode && c.weeklyPickupDay == selectedDay).ToList();
            coordsViewBagMaker(customerList);            

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
