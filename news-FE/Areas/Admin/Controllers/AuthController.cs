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

namespace news_FE.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(AuthRequest auth)
        {    
            JObject loginInfo = new JObject
            {
                { "Username", auth.Username },
                { "Password", auth.Password },              
            };
            string authRequest = SendRequest.sendRequestPOSTwithJsonContent(ApiUrl.urlAuthen, loginInfo.ToString());
            ObjectResult<User> repons = JsonConvert.DeserializeObject<ObjectResult<User>>(authRequest);
            if(repons.code == 200)
            {
                Session["access_token"] = repons.data.token;
                //Session["userName"] =  repons.data.data.username+"";
                //Session["fullName"] = repons.data.data.fullname + "";
                return Redirect("~/admin");
            }
            else
            {
                ViewBag.error = repons.message.Title +" : "+ repons.message.Message;
                return View("login");
            }
      
        }
        public ActionResult Unauthorized()
        {
            return View("Unauthorized");
        }
        // GET: Admin/Auth
        public ActionResult logout()
        {
            Session["access_token"] = "";
            return RedirectToAction("login");
        }
  
    }
}