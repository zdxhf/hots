﻿using System;
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

        [HttpGet]
        public  List<GameNews> getNews(int act)
        {
            List<GameNews> news = new List<GameNews>();
            
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM gamenews order by id desc limit "+act+",2", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while(reader.Read())
                {
                    GameNews gu = new GameNews();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        PropertyInfo property = gu.GetType().GetProperty(reader.GetName(i));
                        property.SetValue(gu, (reader.IsDBNull(i)) ? null : reader.GetValue(i), null);
                    }
                    news.Add(gu);
                }
                reader.Close();
                return news;

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
        [HttpGet]
        public GameNews getLastNews()
        {
            GameNews gu = new GameNews();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM gamenews order by date desc limit 0,1" , conn);
                conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow);
                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo property = gu.GetType().GetProperty(reader.GetName(i));
                    property.SetValue(gu, (reader.IsDBNull(i)) ? null : reader.GetValue(i), null);

                }
                reader.Close();
                return gu;

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



        //[HttpGet]
        //public GameNews Nav(int act)
        //{
        //    return this.getUpdateById(act);
        //}

        
    }
}