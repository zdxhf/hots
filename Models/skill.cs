using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace hotsAPI.Models
{
    public class SimpleSkill
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("技能名字")]
        public string Name { get; set; }

        [DisplayName("对应英雄ID")]
        public int HeroID { get; set; }
    }

    public class Skill
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("技能名字")]
        public string Name { get; set; }

        [DisplayName("图标")]
        public string Icon { get; set; }

        [DisplayName("Category")]
        public int Category { get; set; }

        [DisplayName("热键")]
        public string HotKey { get; set; }

        [DisplayName("能量消耗")]
        public double ManaConsume { get; set; }

        [DisplayName("冷却时间")]
        public double CoolDown { get; set; }

        [DisplayName("描述")]
        public string Description { get; set; }

        [DisplayName("对应英雄ID")]
        public int HeroID { get; set; }
    }
}