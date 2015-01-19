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
        [DisplayName("原帖链接")]
        public string Link { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("资讯类型")]
        public string Type { get; set; }
        
        [Required]
        [DataMember(IsRequired = true)]
        [DisplayName("主题")]
        public string Title { get; set; }
    }
}