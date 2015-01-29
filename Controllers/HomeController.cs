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
        public ActionResult StarsBlue()
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
            //List<SelectListItem> typeList = new List<SelectListItem>();
            //typeList.Add(new SelectListItem { Text = "蓝贴发布", Value = "蓝贴发布" });
            //typeList.Add(new SelectListItem { Text = "前瞻/资讯", Value = "前瞻/资讯" });
            //typeList.Add(new SelectListItem { Text = "新闻/活动", Value = "新闻/活动" });
            //typeList.Add(new SelectListItem { Text = "回顾/统计", Value = "回顾/统计" });
            //typeList.Add(new SelectListItem { Text = "其他相关", Value = "其他相关" });
            //ViewData["TypeList"] = new SelectList(typeList, "Value", "Text", "");
            return View();
        }

        [HttpPost]
        public ActionResult AddRecords(GameNews gn)
        {
            gn.Date = DateTime.Now.ToString();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);           
            conn.Open();
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = trans;
                if (gn.KeyWords != null)
                {
                    //string keywords = gn.KeyWords;
                    string[] keys = gn.KeyWords.Split('.');
                    foreach (string key in keys)
                    {
                        if (key != "")
                        {
                            cmd.CommandText = "SELECT * FROM KeyWords where `From`='" + gn.From + "' and KeyName='" + key + "'";
                           // cmd = new MySqlCommand("SELECT * FROM KeyWords where `From`='"+gn.From+"' and KeyName='" +key+"'", conn);                       
                            MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow);
                            if (!reader.Read())
                            {
                                reader.Close();
                                ArrayList paraList1 = new ArrayList();
                                cmd.CommandText = "INSERT INTO KeyWords (`FROM`,`KeyName`,`RecordTimes`,`Date`) VALUES(@FROM,@KeyName,@RecordTimes,@Date)";
                                MySqlParameter p1 = new MySqlParameter("@FROM", gn.From);
                                MySqlParameter p2 = new MySqlParameter("@KeyName", key);
                                MySqlParameter p3 = new MySqlParameter("@RecordTimes", 1);
                                MySqlParameter p4 = new MySqlParameter("@Date", DateTime.Now.ToShortDateString());
                                paraList1.Add(p1);
                                paraList1.Add(p2);
                                paraList1.Add(p3);
                                paraList1.Add(p4);
                                for (int j = 0; j < paraList1.Count; j++)
                                {
                                    cmd.Parameters.Add((object)paraList1[j]);
                                }
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {                           
                                int id = Int16.Parse(reader["Id"].ToString());
                                int times =Int16.Parse(reader["RecordTimes"].ToString());
                                reader.Close();
                                times++;
                                cmd.CommandText = "update KeyWords set RecordTimes=" + times + " where id=" + id;
                                cmd.ExecuteNonQuery();
                            }
                            reader.Close();
                        }
                    }
                }
                 
                ArrayList paraList2 = new ArrayList();
                cmd.CommandText="INSERT INTO gamenews (`Title`,`FROM`,`Link1`,`Date`,`WebSite1`,`Link2`,`WebSite2`,`IssueDate`,`KeyWord`) VALUES(@Title,@From1,@Link1,@Date1,@WebSite1,@Link2,@WebSite2,@IssueDate,@KeyWord)";
                MySqlParameter p12 = new MySqlParameter("@Title", gn.Title);
                MySqlParameter p13 = new MySqlParameter("@From1", gn.From);
                MySqlParameter p14 = new MySqlParameter("@Link1", gn.Link1);
                MySqlParameter p15 = new MySqlParameter("@Date1", gn.Date);
                MySqlParameter p16 = new MySqlParameter("@WebSite1", gn.WebSite1);
                MySqlParameter p17 = new MySqlParameter("@Link2", gn.Link2);
                MySqlParameter p18 = new MySqlParameter("@WebSite2", gn.WebSite2);
                MySqlParameter p19 = new MySqlParameter("@IssueDate", gn.IssueDate);
                MySqlParameter p10 = new MySqlParameter("@KeyWord", gn.KeyWords);
                paraList2.Add(p12);
                paraList2.Add(p13);
                paraList2.Add(p14);
                paraList2.Add(p15);
                paraList2.Add(p16);
                paraList2.Add(p17);
                paraList2.Add(p18);
                paraList2.Add(p19);
                paraList2.Add(p10);
                for (int j = 0; j < paraList2.Count; j++)
                {
                    cmd.Parameters.Add((object)paraList2[j]);
                }
                cmd.ExecuteNonQuery();
                trans.Commit();
                string act = "";
                switch (gn.From)
                {
                    case "风暴英雄":
                        act = "Storm";
                        break;
                    case "魔兽世界":
                        act = "Wow";
                        break;
                    case "炉石传说":
                        act = "Hots";
                        break;
                    case "星际争霸":
                        act = "Stars";
                        break;
                    case "魔兽争霸":
                        act = "Wars";
                        break;
                    case "暗黑破坏神":
                        act = "Diable";
                        break;
                    case "其他相关":
                        act = "Others";
                        break;
                    default:
                        break;
                }

                return RedirectToAction(act+"Blue");
            }
            catch(Exception)
            {
                trans.Rollback();
                return View();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        [HttpGet]
        public ActionResult Search(string Key)
        {
            string key = Key;
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
