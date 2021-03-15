using news_FE.Models;
using news_FE.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace news_FE.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Admin/Users
            public ActionResult Index()
            {
                string getJsonRepons = SendRequest.sendRequestGET("https://localhost:44313/api/Users/getAllUser", null);
                var List = JsonConvert.DeserializeObject<List<User>>(getJsonRepons);
                return View(List);
            }
        
    }
}