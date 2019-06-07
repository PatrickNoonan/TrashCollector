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
        //public ActionResult PaymentWithPaypal(string Cancel = null)
        //{
        //    //getting the apiContext
        //    APIContext apiContext = PaypalConfiguration.GetAPIContext();

        //    try
        //    {
        //        //A resource representing a Payer that funds a payment Payment Method as paypal
        //        //Payer Id will be returned when payment proceeds or click to pay
        //        string payerId = Request.Params["PayerID"];

        //        if (string.IsNullOrEmpty(payerId))
        //        {
        //            //this section will be executed first because PayerID doesn't exist
        //            //it is returned by the create function call of the payment class

        //            // Creating a payment
        //            // baseURL is the url on which paypal sendsback the data.
        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
        //                        "/Home/PaymentWithPayPal?";

        //            //here we are generating guid for storing the paymentID received in session
        //            //which will be used in the payment execution

        //            var guid = Convert.ToString((new Random()).Next(100000));

        //            //CreatePayment function gives us the payment approval url
        //            //on which payer is redirected for paypal account payment

        //            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

        //            //get links returned from paypal in response to Create function call

        //            var links = createdPayment.links.GetEnumerator();

        //            string paypalRedirectUrl = null;

        //            while (links.MoveNext())
        //            {
        //                Links lnk = links.Current;

        //                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    //saving the payapalredirect URL to which user will be redirected for payment
        //                    paypalRedirectUrl = lnk.href;
        //                }
        //            }

        //            // saving the paymentID in the key guid
        //            Session.Add(guid, createdPayment.id);

        //            return Redirect(paypalRedirectUrl);
        //        }
        //        else
        //        {

        //            // This function exectues after receving all parameters for the payment

        //            var guid = Request.Params["guid"];

        //            var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

        //            //If executed payment failed then we will show payment failure message to user
        //            if (executedPayment.state.ToLower() != "approved")
        //            {
        //                return View("FailureView");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("FailureView");
        //    }

        //    //on successful payment, show success page to user.
        //    return View("SuccessView");
        //}

        //private PayPal.Api.Payment payment;
        //private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        //{
        //    var paymentExecution = new PaymentExecution() { payer_id = payerId };
        //    this.payment = new Payment() { id = paymentId };
        //    return this.payment.Execute(apiContext, paymentExecution);
        //}

        //private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        //{

        //    //create itemlist and add item objects to it
        //    var itemList = new ItemList() { items = new List<Item>() };

        //    //Adding Item Details like name, currency, price etc
        //    itemList.items.Add(new Item()
        //    {
        //        name = "Item Name comes here",
        //        currency = "USD",
        //        price = "1",
        //        quantity = "1",
        //        sku = "sku"
        //    });

        //    var payer = new Payer() { payment_method = "paypal" };

        //    // Configure Redirect Urls here with RedirectUrls object
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl + "&Cancel=true",
        //        return_url = redirectUrl
        //    };

        //    // Adding Tax, shipping and Subtotal details
        //    var details = new Details()
        //    {
        //        tax = "1",
        //        shipping = "1",
        //        subtotal = "1"
        //    };

        //    //Final amount with details
        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = "3", // Total must be equal to sum of tax, shipping and subtotal.
        //        details = details
        //    };

        //    var transactionList = new List<Transaction>();
        //    // Adding description about the transaction
        //    transactionList.Add(new Transaction()
        //    {
        //        description = "Transaction description",
        //        invoice_number = "your generated invoice number", //Generate an Invoice No
        //        amount = amount,
        //        item_list = itemList
        //    });


        //    this.payment = new Payment()
        //    {
        //        intent = "sale",
        //        payer = payer,
        //        transactions = transactionList,
        //        redirect_urls = redirUrls
        //    };

        //    // Create a payment using a APIContext
        //    return this.payment.Create(apiContext);
        //}
    }
}
