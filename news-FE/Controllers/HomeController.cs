using news_FE.Models;
using news_FE.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace news_FE.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {       
            //string getJsonRepons = SendRequest.sendRequestGET("https://localhost:44313/api/Posts/getAllPost",null);
            //var ListPost = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons);
            return View("");
        }
    }
}