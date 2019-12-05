using Intex.DAL;
using Intex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {

        NorthwestContext db = new NorthwestContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuoteCalculator()
        {
            List<Assay> assayNames = db.Assay.ToList();
            ViewBag.AssayNames = assayNames;
   
            return View();
        }

        [HttpPost]
        public ActionResult GetQuote(FormCollection formQuote) 
         {
            if (formQuote ==null)
            {
                return HttpNotFound();
            }
            else
            {
                FormCollection myForm = formQuote;
                if (myForm ==null || myForm["Compound Weight"]=="" || myForm["Due Date"] == "")
                {
                    ViewBag.CantComplete = "Please make sure you've filled out all of the information below ";
                    List<Assay> assayNames = db.Assay.ToList();
                    ViewBag.AssayNames = assayNames;
                    return View("QuoteCalculator");
                }
                //store the form collection inputs into variable, as well as calculate the the days difference
                double weight = Convert.ToDouble(formQuote["Compound Weight"]);
                string assayName = Convert.ToString(formQuote["Assay Names"]);
                bool condTest = Convert.ToBoolean(myForm["NeedsTesting"].Split(',')[0]);
                DateTime dueDate = Convert.ToDateTime(formQuote["Due Date"]);
                int daysDiff = dueDate.Day - DateTime.Now.Day;
                
                //base price
                double price = weight * 10;
              
                //prices change depedning on which type they are
                if (assayName == "Biochemical Pharmacology")
                {
                    price = price + 200;
                }
                else if (assayName == "DiscoveryScreen")
                {
                    price = price + 100;
                }
                else if (assayName == "ImmunoScreen")
                {
                    price = price + 300;
                }
                else if (assayName == "ProfilingScreen")
                {
                    price = price + 200;
                }
                else if (assayName == "PharmaScreen")
                {
                    price = price + 150;
                }
                else if (assayName == "CustomScreen")
                {
                    price = price + 500;
                }

                //price changes depending on how many days they need it done by
                if (daysDiff > 14)
                {

                }
                else if (daysDiff >= 10)
                {
                    price *= 1.05;
                }
                else if (daysDiff >= 5)
                {
                    price *= 1.25;
                }
                else if (daysDiff ==4)
                {
                    price *= 1.75;
                }
                //if num days is <4, it displays an error
                else
                {
                    ViewBag.CantComplete = "Due to shipping constraints, please give us at least 4 days from when you sbumit your order to display the results";
                    List<Assay> assayNames = db.Assay.ToList();
                    ViewBag.AssayNames = assayNames;
                    return View("QuoteCalculator");
                }
                //if they need conditional testing, the price is multiplied by two
                if (condTest == true)
                {
                    price *= 2;
                }
                //viewbag for the finished quote view
                ViewBag.AssayName = assayName;
                ViewBag.Price = price;

                return View("FinishedQuote");
            }
            
        }

        private void RedirectToAction(HttpNotFoundResult httpNotFoundResult)
        {
            throw new NotImplementedException();
        }
    }
}