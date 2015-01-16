using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hotsAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BlzBlue()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Index(hotsAPI.Models.Hero hreo)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        string a = "aa";
        //    }
        //    string aa = "dd";
        //    return null;
        //}
        //[HttpGet]
        //public ActionResult AddHero()
        //{
        //    return View();
        //}
        public ActionResult ApiManual()
        {
            return View();
        }
    }
}
