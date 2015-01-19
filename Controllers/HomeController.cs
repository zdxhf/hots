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
        public ActionResult AddRecords()
        {
            List<SelectListItem> fromList = new List<SelectListItem>();
            fromList.Add(new SelectListItem { Text = "风暴英雄", Value = "风暴英雄" });
            fromList.Add(new SelectListItem { Text = "魔兽世界", Value = "魔兽世界" });
            fromList.Add(new SelectListItem { Text = "炉石传说", Value = "炉石传说" });
            fromList.Add(new SelectListItem { Text = "星际争霸", Value = "星际争霸" });
            fromList.Add(new SelectListItem { Text = "魔兽争霸", Value = "魔兽争霸" });
            fromList.Add(new SelectListItem { Text = "其他相关", Value = "其他相关" });
            ViewData["FromList"] = new SelectList(fromList, "Value", "Text", "");

            List<SelectListItem> typeList = new List<SelectListItem>();
            typeList.Add(new SelectListItem { Text = "蓝贴发布", Value = "蓝贴发布" });
            typeList.Add(new SelectListItem { Text = "前瞻咨询", Value = "前瞻咨询" });
            typeList.Add(new SelectListItem { Text = "数据统计", Value = "数据统计" });
            typeList.Add(new SelectListItem { Text = "其他相关", Value = "其他相关" });
            ViewData["TypeList"] = new SelectList(typeList, "Value", "Text", "");
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
