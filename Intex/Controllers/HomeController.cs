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
                double weight = Convert.ToDouble(formQuote["Compound Weight"]);
                string assayName = Convert.ToString(formQuote["Assay Names"]);

                double price = weight * 10;

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