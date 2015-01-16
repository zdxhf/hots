using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace hotsAPI.Models
{
    public class SimpleTalent
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("技能名字")]
        public string Name { get; set; }
        
    }

    public class Talent
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("技能名字")]
        public string Name { get; set; }

        [DisplayName("图标")]
        public string Icon { get; set; }

        [DisplayName("Level")]
        public int Level { get; set; }

        [DisplayName("EffectSkillId")]
        public int EffectSkillId { get; set; }

        [DisplayName("描述")]
        public string Description { get; set; }

    }
}