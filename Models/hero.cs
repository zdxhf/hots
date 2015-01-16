using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace hotsAPI.Models
{
    public class SimpleHero //只包括两个字段，用于查询
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("英雄名字")]
        public string Name { get; set; }

    }

    [DataContract]
    public class Hero
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("英雄名字")]
        public string Name { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("生命值")]
        public int Life { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("生命恢复")]
        public double LifeRestore { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("能量")]
        public int Energy { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("能量恢复")]
        public double EnergyRestore { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("攻击力")]
        public int Attack { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("攻击速度")]
        public double AttackSpeed { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("攻击范围")]
        public double AttackRange { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("移动速度")]
        public double MoveSpeed { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("称号")]
        public string Title { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("角色分类")]
        public int RoleCategory { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("游戏分类")]
        public int GameCategory { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("描述")]
        public string Descritption { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("头像图标")]
        public string HeaderIcon { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("身体图标")]
        public string BodyImage { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("升级生命值")]
        public int LifeUpgrade { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("升级生命恢复值")]
        public double LifeRestoreUpgrade { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("升级能量值")]
        public int EnergyUpgrade { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("升级能量恢复值")]
        public double EnergyRestoreUpgrade { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("升级攻击力")]
        public int AttackUpgrade { get; set; }
    }
}