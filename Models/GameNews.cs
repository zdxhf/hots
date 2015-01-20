using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace hotsAPI.Models
{
    public class GameNews
    {
        public int ID { get; set; }
        public string Date { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("游戏分类")]
        public string From { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("引用网站一")]
        public string WebSite1 { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("详细链接一")]
        public string Link1 { get; set; }

        [DisplayName("引用网站二")]
        public string WebSite2 { get; set; }

        [DisplayName("详细链接二")]
        public string Link2 { get; set; }


        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("资讯类型")]
        public string Type { get; set; }
        
        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("主题内容")]
        public string Title { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("发布日期")]
        public string IssueDate { get; set; }
    }
}