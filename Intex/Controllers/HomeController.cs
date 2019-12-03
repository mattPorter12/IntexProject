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

        NorthwestContext db = new NorthwestContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuoteCalculator()
        {

            return View();
        }
    }
}