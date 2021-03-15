using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace news_FE.Models
{
    public class AuthRequest
    { 
        public string Username { get; set; }
        public string Password { get; set; }
    }
}