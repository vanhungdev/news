using news_FE.consts;
using news_FE.library;
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
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllUser, null);
            List<User> ListUser = null;
            try
            {
                ListUser = JsonConvert.DeserializeObject<List<User>>(getJsonRepons);
            }
            catch
            {
                var objectResult = JsonConvert.DeserializeObject<ObjectResult<User>>(getJsonRepons);
                Message.set_flash( objectResult.message.Message, "danger");
                return RedirectToAction("Unauthorized", "Auth");
            }
            if (ListUser != null)
            {
                return View(ListUser.OrderByDescending(m => m.ID));
            }
            else
            {
                Message.set_flash("Đã xảy ra lỗi", "danger");
                return RedirectToAction("Unauthorized", "Auth");
            }
        }

    }
}