using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hotsAPI.Models
{
    public class GameKey
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public string KeyName { get; set; }
        public int RecordTimes { get; set; }
    }
}