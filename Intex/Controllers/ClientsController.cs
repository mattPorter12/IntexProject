using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Intex.DAL;
using Intex.Models;

namespace Intex.Controllers
{
    public class ClientsController : Controller
    {
        //initialize db of type NorthwestContext
        private NorthwestContext db = new NorthwestContext();

        // Get login method 
        public ActionResult Login()
        {
            if(ViewBag.ErrorMessage == null)
            {
                ViewBag.ErrorMessage = "";
            }
            return View();
        }

        //Post login method checks username and password and sets cookie
        [HttpPost]
        public ActionResult Login(Login name, bool rememberMe= false)
        {
            //check client username
            Login success = db.Login.Find(name.UserName);
            if (success != null)
            {
                   //check client password
                if (success.Password == name.Password)
                {
                    //set cookie and redirect to client portal
                    FormsAuthentication.SetAuthCookie(name.UserName, rememberMe);
                    return RedirectToAction("Index", "WorkOrders", new { id = success.ClientID});
                }
            }
            ViewBag.ErrorMessage = "The Username and Password are not valid";
            return View("Login");
        }

        //Transfer client to new account view
        [HttpGet]
        public ActionResult NewAccount()
        {
            return View();
        }

        // Get method redirects to create client view
        public ActionResult Create()
        {
            return View();
        }

        // Post method adds client information to create a client portal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,ClientName,PhysAddress1,PhysAddress2,PhysCity,PhysState,PhysZipCode,PointOfContact,PointPhoneNum,DiscountRate,Balance")] Client client)
        {
            if (ModelState.IsValid)
            {
                //add client model
                db.Client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // Get method locates and returns client ID edit view
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // Post method edits client information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,ClientName,PhysAddress1,PhysAddress2,PhysCity,PhysState,PhysZipCode,PointOfContact,PointPhoneNum,DiscountRate,Balance")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // Get method locates and returns client id to delete view
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // Post method locates client id and deletes record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            db.Client.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
