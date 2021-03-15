using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace news_FE.Models
{
    public  class Role
    {
        public int Id { get; set; }
        public int Access { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }
}