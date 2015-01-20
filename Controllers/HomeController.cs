using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hotsAPI.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections;
using System.Data;

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

        [HttpPost]
        [HandleError]
        public ActionResult AddRecords(GameNews gn)
        {
            gn.Date = DateTime.Now.ToString();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                ArrayList paraList1 = new ArrayList();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO gamenews (`Type`,`Title`,`FROM`,`Link`,`Date`) VALUES(@Type,@Title,@From,@Link,@Date)", conn);
                conn.Open();
                MySqlParameter p1 = new MySqlParameter("@Type", gn.Type);
                MySqlParameter p2 = new MySqlParameter("@Title", gn.Title);
                MySqlParameter p3 = new MySqlParameter("@From", gn.From);
                MySqlParameter p4 = new MySqlParameter("@Link", gn.Link);
                MySqlParameter p5 = new MySqlParameter("@Date", gn.Date);
                paraList1.Add(p1);
                paraList1.Add(p2);
                paraList1.Add(p3);
                paraList1.Add(p4);
                paraList1.Add(p5);
                for (int j = 0; j < paraList1.Count; j++)
                {
                    cmd.Parameters.Add((object)paraList1[j]);
                }
                cmd.ExecuteNonQuery();
                return RedirectToAction("BlzBlue");
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
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
