using Intex.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intex.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var db = new NorthwestContext();
            db.SaveChanges();
            return View();
        }
        public ActionResult QuoteCalculator()
        {

            return View();
        }
    }
}