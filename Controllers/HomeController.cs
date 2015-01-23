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
    [HandleError]
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
        public ActionResult WowBlue()
        {
            return View();
        }
        public ActionResult StormBlue()
        {
            return View();
        }
        public ActionResult HotsBlue()
        {
            return View();
        }
        public ActionResult StarBlue()
        {
            return View();
        }
        public ActionResult WarsBlue()
        {
            return View();
        }
        public ActionResult DiableBlue()
        {
            return View();
        }
        public ActionResult OthersBlue()
        {
            return View();
        }
        //public ActionResult AddRecords()
        //{
        //    List<SelectListItem> fromList = new List<SelectListItem>();
        //    fromList.Add(new SelectListItem { Text = "风暴英雄", Value = "风暴英雄" });
        //    fromList.Add(new SelectListItem { Text = "魔兽世界", Value = "魔兽世界" });
        //    fromList.Add(new SelectListItem { Text = "炉石传说", Value = "炉石传说" });
        //    fromList.Add(new SelectListItem { Text = "星际争霸", Value = "星际争霸" });
        //    fromList.Add(new SelectListItem { Text = "魔兽争霸", Value = "魔兽争霸" });
        //    fromList.Add(new SelectListItem { Text = "其他相关", Value = "其他相关" });
        //    ViewData["FromList"] = new SelectList(fromList, "Value", "Text", "");

        //    List<SelectListItem> typeList = new List<SelectListItem>();
        //    typeList.Add(new SelectListItem { Text = "蓝贴发布", Value = "蓝贴发布" });
        //    typeList.Add(new SelectListItem { Text = "前瞻咨询", Value = "前瞻咨询" });
        //    typeList.Add(new SelectListItem { Text = "数据统计", Value = "数据统计" });
        //    typeList.Add(new SelectListItem { Text = "其他相关", Value = "其他相关" });
        //    ViewData["TypeList"] = new SelectList(typeList, "Value", "Text", "");
        //    return View();
        //}
        [HttpGet]
        public ActionResult AddRecords(string catelog)
        {
            List<SelectListItem> fromList = new List<SelectListItem>();
            fromList.Add(new SelectListItem { Text = "风暴英雄", Value = "风暴英雄" });
            fromList.Add(new SelectListItem { Text = "魔兽世界", Value = "魔兽世界" });
            fromList.Add(new SelectListItem { Text = "炉石传说", Value = "炉石传说" });
            fromList.Add(new SelectListItem { Text = "星际争霸", Value = "星际争霸" });
            fromList.Add(new SelectListItem { Text = "魔兽争霸", Value = "魔兽争霸" });
            fromList.Add(new SelectListItem { Text = "暗黑破坏神", Value = "暗黑破坏神" });
            fromList.Add(new SelectListItem { Text = "其他相关", Value = "其他相关" });
            switch (catelog)
            {
                case "Storm":
                    ViewData["From"] = "风暴英雄";                  
                    break;
                case "Wow":
                    ViewData["From"] = "魔兽世界";
                    break;
                case "Hots":
                    ViewData["From"] = "炉石传说";
                    break;
                case "Stars":
                    ViewData["From"] = "星际争霸";
                    break;
                case "Wars":
                    ViewData["From"] = "魔兽争霸";
                    break;
                case "Diable":
                    ViewData["From"] = "暗黑破坏神";
                    break;
                case "Others":
                    ViewData["From"] = "其他相关";
                    break;
                default:
                    break;
            }
            ViewData["FromList"] = new SelectList(fromList, "Value", "Text", "");
            List<SelectListItem> typeList = new List<SelectListItem>();
            typeList.Add(new SelectListItem { Text = "蓝贴发布", Value = "蓝贴发布" });
            typeList.Add(new SelectListItem { Text = "前瞻/资讯", Value = "前瞻/资讯" });
            typeList.Add(new SelectListItem { Text = "新闻/活动", Value = "新闻/活动" });
            typeList.Add(new SelectListItem { Text = "回顾/统计", Value = "回顾/统计" });
            typeList.Add(new SelectListItem { Text = "其他相关", Value = "其他相关" });
            ViewData["TypeList"] = new SelectList(typeList, "Value", "Text", "");

            return View();
        }

        [HttpPost]
        public ActionResult AddRecords(GameNews gn)
        {
            gn.Date = DateTime.Now.ToString();
            if (gn.KeyWords != null)
            {
                string keywords = gn.KeyWords;
                if ((keywords.StartsWith(",")))
                {
                    gn.KeyWords = keywords.Substring(1);
                }
            }
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                ArrayList paraList1 = new ArrayList();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO gamenews (`Type`,`Title`,`FROM`,`Link1`,`Date`,`WebSite1`,`Link2`,`WebSite2`,`IssueDate`,`KeyWord`) VALUES(@Type,@Title,@From,@Link1,@Date,@WebSite1,@Link2,@WebSite2,@IssueDate,@KeyWord)", conn);
                conn.Open();
                MySqlParameter p1 = new MySqlParameter("@Type", gn.Type);
                MySqlParameter p2 = new MySqlParameter("@Title", gn.Title);
                MySqlParameter p3 = new MySqlParameter("@From", gn.From);
                MySqlParameter p4 = new MySqlParameter("@Link1", gn.Link1);
                MySqlParameter p5 = new MySqlParameter("@Date", gn.Date);
                MySqlParameter p6 = new MySqlParameter("@WebSite1", gn.WebSite1);
                MySqlParameter p7 = new MySqlParameter("@Link2", gn.Link2);
                MySqlParameter p8 = new MySqlParameter("@WebSite2", gn.WebSite2);
                MySqlParameter p9 = new MySqlParameter("@IssueDate", gn.IssueDate);
                MySqlParameter p10 = new MySqlParameter("@KeyWord", gn.KeyWords);
                paraList1.Add(p1);
                paraList1.Add(p2);
                paraList1.Add(p3);
                paraList1.Add(p4);
                paraList1.Add(p5);
                paraList1.Add(p6);
                paraList1.Add(p7);
                paraList1.Add(p8);
                paraList1.Add(p9);
                paraList1.Add(p10);
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
