using news_FE.consts;
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
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllPost, null);
            var ListPost = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons);
            return View();
        }
    }
}