using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intex.DAL;
using Intex.Models;

namespace Intex.Controllers
{
    public class InvoicesController : AuthorizedController
    {
        private NorthwestContext db = new NorthwestContext();
        public static decimal subtotal;
        // GET: Invoices
        public ActionResult Index()
        {
            Login name = new Login();
            name = db.Login.Find(User.Identity.Name);
            int nameID = name.ClientID;

            IEnumerable<Invoice> theInvoices = db.Database.SqlQuery<Invoice>("SELECT * FROM Invoice WHERE ClientID = " + nameID + " ORDER BY DueDate DESC;");
            return View(theInvoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoice.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        public ActionResult Create()
        {
            Login theClient = db.Login.Find(User.Identity.Name);
            int clientNum = theClient.ClientID;
            Client ourClient = new Client();
            ourClient = db.Client.Find(clientNum);

            IEnumerable<WorkOrder> clientsOrders = db.Database.SqlQuery<WorkOrder>("SELECT * FROM WorkOrder WHERE ClientID = " + clientNum + ";");

            IEnumerable<Invoice> theInvoices;
            if (ourClient.Balance == 0 || ourClient.Balance == null)
            {
                theInvoices = db.Database.SqlQuery<Invoice>("SELECT * FROM Invoice WHERE PaymentStatus != 'Paid'");
                subtotal = 0;
                foreach (var item in theInvoices)
                {
                    subtotal += item.TotalMatCost;
                }
                var sql = "UPDATE Client SET Balance = @subtotal WHERE ClientID = @clientID;";
                db.Database.ExecuteSqlCommand(sql, new SqlParameter("@clientID", clientNum), new SqlParameter("@subtotal", subtotal));

                ViewBag.InvoiceOutput = subtotal;
            }
            else
            {
                Client newClient = new Client();
                newClient = db.Client.Find(clientNum);

                ViewBag.InvoiceOutput = newClient.Balance;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection paymentAmount)
        {
            if (paymentAmount == null)
            {
                return HttpNotFound();
            }
            else
            {
                decimal payment = Convert.ToDecimal(paymentAmount["Payment Amount"]);
                subtotal = subtotal - payment;

                //ViewBag.InvoiceOutput = subtotal;
                Session["subTotal"] = subtotal;
                return RedirectToAction("CreateAgain", "Invoices");
            }
        }

        public ActionResult CreateAgain()
        {
            ViewBag.InvoiceOutput = Session["subTotal"];
            return View();
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
