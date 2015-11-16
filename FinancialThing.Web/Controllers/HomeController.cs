using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialThing.DataAccess;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Newtonsoft.Json;

namespace FinancialThing.Controllers
{
    public class HomeController : Controller
    {
        private IDataGrabber _grabber;

        public HomeController(IDataGrabber grabber)
        {
            _grabber = grabber;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Tables()
        {
            return View();
        }

        public string GetCompanies()
        {
            var grabber = new FTDataGrabber();
            var str = grabber.Grab("http://localhost:53357/api/company/");
            return str;
        }

        
    }
}