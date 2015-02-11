using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using hotsAPI.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Text;
using System.Collections;

namespace hotsAPI.Controllers
{
    public class apiController : ApiController
    {
        private List<SimpleHero> getAllHeros()
        {
            List<SimpleHero> Heros = new List<SimpleHero>();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT ID,Name FROM heros order by id", conn);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    SimpleHero hero = new SimpleHero();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = hero.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(hero, (reader.IsDBNull(i)) ? "[NULL]" : reader.GetValue(i), null);
                    }
                    Heros.Add(hero);
                }
                reader.Close();
                return Heros;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        private Hero getHeroById(int id)
        {
            Hero hero = new Hero();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM heros where id=" + id, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = hero.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(hero, (reader.IsDBNull(i)) ? "[NULL]" : reader.GetValue(i), null);
                    }
                }
                reader.Close();
                return hero;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        // GET api/GetHero/-1 返回所有 其他返回单个
        [HttpGet]
        public HttpResponseMessage GetHero(int id)
        {
            if (id < -1)
            {
                return null;
            }
            if (id==-1)
            {
                List<SimpleHero> Heros = getAllHeros();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = serializer.Serialize(Heros);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                Hero Hero = getHeroById(id);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = serializer.Serialize(Hero);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
        }

        // GET api/GetHero/5?callback=?  JSONP调用 -1返回所有 其他返回单个
        [HttpGet]
        public HttpResponseMessage GetHero(string callback,int id)
        {
            if (id < -1)
            {
                return null;
            }
            if (id == -1)
            {
                List<SimpleHero> Heros = getAllHeros();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = string.Format("{0}({1})", callback, serializer.Serialize(Heros));
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                Hero Hero = getHeroById(id);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = string.Format("{0}({1})", callback, serializer.Serialize(Hero));
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
        }
        private List<SimpleSkill> getAllSkill()
        {
            List<SimpleSkill> skills = new List<SimpleSkill>();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT ID,Name,HeroID FROM skills order by id, heroid", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    SimpleSkill skill = new SimpleSkill();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = skill.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(skill, (reader.IsDBNull(i)) ? "[NULL]" : reader.GetValue(i), null);
                    }
                    skills.Add(skill);
                }
                reader.Close();
                return skills;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        private Skill getSkillById(int id)
        {
            Skill skill = new Skill();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM skills where id =" + id, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = skill.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(skill, (reader.IsDBNull(i)) ? null : reader.GetValue(i), null);
                    }
                }
                reader.Close();
                return skill;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        // GET api/GetSkill/-1 返回所有 其他返回单个
        [HttpGet]
        public HttpResponseMessage GetSkill(int id)
        {
            if (id < -1)
            {
                return null;
            }
            if (id == -1)
            {
                List<SimpleSkill> skills = getAllSkill();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = serializer.Serialize(skills);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                Skill skill = getSkillById(id);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = serializer.Serialize(skill);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
        }
        // GET api/GetSkill/5?callback=?  JSONP调用 -1返回所有 其他返回单个
        [HttpGet]
        public HttpResponseMessage GetSkill(string callback, int id)
        {
            if (id < -1)
            {
                return null;
            }
            if (id == -1)
            {
                List<SimpleSkill> skills = getAllSkill();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = string.Format("{0}({1})", callback, serializer.Serialize(skills));
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                Skill skill = getSkillById(id);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = string.Format("{0}({1})", callback, serializer.Serialize(skill));
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
        }


        private List<SimpleTalent> getAllTalent()
        {
            List<SimpleTalent> talents = new List<SimpleTalent>();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT ID,Name FROM talent order by id", conn);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    SimpleTalent talent = new SimpleTalent();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = talent.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(talent, (reader.IsDBNull(i)) ? "[NULL]" : reader.GetValue(i), null);
                    }
                    talents.Add(talent);
                }
                reader.Close();
                return talents;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }      
        private Talent getTalentById(int id)
        {
            Talent talent = new Talent();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM talent where id =" + id, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow);

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = talent.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(talent, (reader.IsDBNull(i)) ? null : reader.GetValue(i), null);
                    }
                }
                reader.Close();
                return talent;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }


        // GET api/GetTalent/-1 返回所有 其他返回单个
        [HttpGet]
        public HttpResponseMessage GetTalent(int id)
        {
            if (id < -1)
            {
                return null;
            }
            if (id == -1)
            {
                List<SimpleTalent> skills = getAllTalent();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = serializer.Serialize(skills);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                Talent talent = getTalentById(id);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = serializer.Serialize(talent);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
        }
        // GET api/GetTalent/5?callback=?  JSONP调用 -1返回所有 其他返回单个
        [HttpGet]
        public HttpResponseMessage GetTalent(string callback, int id)
        {
            if (id < -1)
            {
                return null;
            }
            if (id == -1)
            {
                List<SimpleTalent> talents = getAllTalent();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = string.Format("{0}({1})", callback, serializer.Serialize(talents));
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                Talent talent = getTalentById(id);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string content = string.Format("{0}({1})", callback, serializer.Serialize(talent));
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
            }
        }

        // GET api/GetMacth/2015-1-1
        [HttpGet]
        public List<leiFengMatch> GetMatch(DateTime date)
        {
            List<leiFengMatch> matchs = new List<leiFengMatch>();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM leifengcupherostats where matchdate>='" + date + "'", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    leiFengMatch match = new leiFengMatch();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = match.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(match, (reader.IsDBNull(i)) ? "[NULL]" : reader.GetValue(i), null);
                    }
                    matchs.Add(match);
                }
                reader.Close();
                return matchs;
            }
            catch(Exception)
            {
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        private List<GameNews> News(string sql)
        {
            List<GameNews> news = new List<GameNews>();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql,conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    GameNews gu = new GameNews();
                    gu.From = reader["From"].ToString();
                    //gu.Type = reader["Type"].ToString();
                    gu.Link1 = reader["Link1"].ToString();
                    gu.Link2 = reader["Link2"].ToString();
                    gu.Link3 = reader["Link3"].ToString();
                    gu.WebSite1 = reader["WebSite1"].ToString();
                    gu.WebSite2 = reader["WebSite2"].ToString();
                    gu.WebSite3 = reader["WebSite3"].ToString();
                    gu.IssueDate = DateTime.Parse(reader["IssueDate"].ToString()).ToShortDateString();
                    gu.Title = reader["Title"].ToString();
                    gu.KeyWords =reader["KeyWord"].ToString();
                    news.Add(gu);
                }
                reader.Close();
                return news;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        private int PageNewsNum = 10;//每页的记录数
        [HttpGet]
        public  List<GameNews> getNews(int act)
        {
            string sql = "SELECT * FROM gamenews order by IssueDate desc,id desc limit " + act + "," + PageNewsNum;
            List<GameNews> news = News(sql);
            return news;
        }
        [HttpGet]
        public List<GameNews> getStormNews(int act)
        {
            string sql = "SELECT * FROM gamenews Where `From`='风暴英雄'  order by IssueDate desc,id desc limit " + act + "," + PageNewsNum;
            List<GameNews> news = News(sql);
            return news;
        }
        [HttpGet]
        public List<GameNews> getWowNews(int act)
        {
            string sql = "SELECT * FROM gamenews Where `From`='魔兽世界'  order by  IssueDate desc,id desc limit " + act + "," + PageNewsNum;
            List<GameNews> news = News(sql);
            return news;
        }
        [HttpGet]
        public List<GameNews> getHotsNews(int act)
        {
            string sql = "SELECT * FROM gamenews Where `From`='炉石传说'  order by  IssueDate desc,id desc limit " + act + "," + PageNewsNum;
                List<GameNews> news = News(sql);
                return news;
        }
        [HttpGet]
        public List<GameNews> getNewsFromKey(int act,string from, string keyword)
        {
            string sql = "SELECT * FROM gamenews Where `From`='" + from + "' and KeyWord Like N'%." + keyword + ".%' order by  IssueDate desc,id desc limit " + act + "," + PageNewsNum;
                List<GameNews> news = News(sql);
                return news;
        }
        [HttpGet]
        public List<GameNews> getStarsNews(int act)
        {
            string sql = "SELECT * FROM gamenews Where `From`='星际争霸'  order by  IssueDate desc,id desc limit " + act + "," + PageNewsNum;
            List<GameNews> news = News(sql);
            return news;
        }
        [HttpGet]
        public List<GameNews> getWarsNews(int act)
        {
            string sql = "SELECT * FROM gamenews Where `From`='魔兽争霸'  order by  IssueDate desc,id desc limit " + act + "," + PageNewsNum;
            List<GameNews> news = News(sql);
            return news;
        }
        [HttpGet]
        public List<GameNews> getDiableNews(int act)
        {
            string sql = "SELECT * FROM gamenews Where `From`='暗黑破坏神'  order by  IssueDate desc,id desc limit " + act + "," + PageNewsNum;
            List<GameNews> news = News(sql);
            return news;
        }
        [HttpGet]
        public List<GameNews> getOthersNews(int act)
        {
            string sql = "SELECT * FROM gamenews Where `From`='其他相关'  order by  IssueDate desc,id desc limit " + act + "," + PageNewsNum;
            List<GameNews> news = News(sql);
            return news;
        }
        [HttpGet]
        public List<GameKey> getKeyWords(string from)
        {
            string sql = "SELECT * FROM keywords Where `From`='"+from+"'";
            List<GameKey> keyWords = new List<GameKey>();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    GameKey keyWord = new GameKey();
                    keyWord.KeyName = reader["KeyName"].ToString();
                    keyWord.RecordTimes = Int16.Parse(reader["RecordTimes"].ToString());
                    keyWords.Add(keyWord);
                }
                reader.Close();
                return keyWords;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        //[HttpGet]
        //public List<GameNews> Search(string from,string Key,int act)
        //{
        //    string sql = "SELECT * FROM gamenews Where `From`='"+from+"' and Key='"+Key+"'  order by IssueDate desc limit " + act + "," + PageNewsNum;
        //    List<GameNews> news = News(sql);
        //    return news;
        //}
        //[HttpGet]
        //public GameNews getLastNews()
        //{
        //    GameNews gu = new GameNews();
        //    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("SELECT * FROM gamenews order by date desc limit 0,1" , conn);
        //        conn.Open();

        //        MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow);
        //        reader.Read();
        //        for (int i = 0; i < reader.FieldCount; i++)
        //        {
        //            PropertyInfo property = gu.GetType().GetProperty(reader.GetName(i));
        //            property.SetValue(gu, (reader.IsDBNull(i)) ? null : reader.GetValue(i), null);

        //        }
        //        reader.Close();
        //        return gu;

        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if (conn.State != ConnectionState.Closed)
        //            conn.Close();
        //    }
        //}


        //[HttpPost]
        //public bool addRecords(GameNews gn)
        //{
        //    gn.Date = DateTime.Now.ToString();
        //    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //    try
        //    {
        //        ArrayList paraList1 = new ArrayList();
        //        MySqlCommand cmd = new MySqlCommand("INSERT INTO gamenews (`Type`,`Title`,`FROM`,`Link`,`Date`) VALUES(@Type,@Title,@From,@Link,@Date)", conn);
        //        conn.Open();
        //        MySqlParameter p1 = new MySqlParameter("@Type", gn.Type);
        //        MySqlParameter p2 = new MySqlParameter("@Title", gn.Title);
        //        MySqlParameter p3 = new MySqlParameter("@From", gn.From);
        //        MySqlParameter p4 = new MySqlParameter("@Link", gn.Link);
        //        MySqlParameter p5 = new MySqlParameter("@Date", gn.Date);
        //        paraList1.Add(p1);
        //        paraList1.Add(p2);
        //        paraList1.Add(p3);
        //        paraList1.Add(p4);
        //        paraList1.Add(p5);
        //        for (int j = 0; j < paraList1.Count; j++)
        //        {
        //            cmd.Parameters.Add((object)paraList1[j]);
        //        }
        //        cmd.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        if (conn.State != ConnectionState.Closed)
        //            conn.Close();
        //    }
        //}
        //[HttpGet]
        //public GameNews Nav(int act)
        //{
        //    return this.getUpdateById(act);
        //}

        
    }
}