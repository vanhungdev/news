using news_FE.consts;
using news_FE.library;
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
                Message.set_flash(objectResult.message.Message, "danger");
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
        public ActionResult Edit(int id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetUserById + id, null);
            User user = JsonConvert.DeserializeObject<User>(getJsonRepons);
            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllRole, null);
            ViewBag.role = JsonConvert.DeserializeObject<List<Role>>(getJsonAllTopicRepons);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            JObject topicJson = new JObject
            {
                { "ID", user.ID },
                { "fullname", user.fullname },
                { "username", user.username },
                { "password", user.password },
                { "email", user.email },
                { "gender", user.gender },
                { "address", user.address },
                { "phone", user.phone }, 
                { "img", user.img },
                { "access", user.access },
                { "created_at", user.created_at.ToString("yyyy-MM-ddTHH:mm:ss")},
                { "created_by", user.created_by},
                { "updated_at", user.updated_at.ToString("yyyy-MM-ddTHH:mm:ss")},
                { "updated_by", user.updated_by},
                { "status", user.status},
            };

            string EditResult = SendRequest.sendRequestPOSTwithJsonContent(ApiUrl.urlEditUser, topicJson.ToString());
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<User>>(EditResult);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("index");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }

            return RedirectToAction("index");
        }

        public ActionResult Status(int Id, int Status)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlChangeStatusUser + "?Id=" + Id + "&Status=" + Status, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("index");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult DeTrash(int Id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDeTrashUser + "?Id=" + Id, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("index");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult reTrash(int Id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlReTrashUser + "?Id=" + Id, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<User>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("index");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult delete(int Id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDeleteUser + "?Id=" + Id, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("index");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult Trash()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllUserTrash, null);
            var List = JsonConvert.DeserializeObject<List<User>>(getJsonRepons);
            return View(List);
        }
    }
}