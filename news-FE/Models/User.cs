﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace news_FE.Models
{
    public class User
    {
        public int ID { get; set; }

        public string fullname { get; set; }

        public string username { get; set; }

        public string password { get; set; }
        public string email { get; set; }
        public string gender { get; set; }

        public string address { get; set; }
        public string phone { get; set; }
        public string img { get; set; }

        public int access { get; set; }
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public DateTime updated_at { get; set; }

        public int updated_by { get; set; }

        public int status { get; set; }
    }
}