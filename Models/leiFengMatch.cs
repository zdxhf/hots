using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hotsAPI.Models
{
    public class leiFengMatch
    {
        public int ID { get; set; }

        public int HeroID { get; set; }

        public float AppearanceRate { get; set; }

        public float WinningPercentage { get; set; }

        public string MatchName { get; set; }

        public DateTime MatchDate { get; set; }
    }
}