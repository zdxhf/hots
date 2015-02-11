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
        [DisplayName("引用网站名")]
        [StringLength(10,ErrorMessage="网站名字过长")]
        public string WebSite1 { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("详细链接")]
        public string Link1 { get; set; }

        [StringLength(10, ErrorMessage = "网站名字过长")]
        [DisplayName("引用网站名二")]
        public string WebSite2 { get; set; }

        [DisplayName("详细链接二")]
        public string Link2 { get; set; }

        [StringLength(10, ErrorMessage = "网站名字过长")]
        [DisplayName("引用网站名三")]
        public string WebSite3 { get; set; }

        [DisplayName("详细链接三")]
        public string Link3 { get; set; }


        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("主题内容")]
        [StringLength(50, ErrorMessage = "内容过长")]
        public string Title { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("发布日期")]
        public string IssueDate { get; set; }

        [DisplayName("标签")]
        [StringLength(5, ErrorMessage = "标签过长")]
        public string KeyWord { get; set; }

        [DisplayName("标签")]
        [Required]
        [DataMember(IsRequired = true)]
        public string KeyWords { get; set; }
    }
}