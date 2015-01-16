using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hotsAPI.Models
{
    public class GameNews
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
    }
}