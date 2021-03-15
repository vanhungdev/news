using news_FE.consts;
using news_FE.library;
using news_FE.Models;
using news_FE.Request;
using news_FE.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace news_FE.Areas.Admin.Controllers
{
    public class TopicsController : BaseController
    {
        // GET: Admin/Topics

        public ActionResult Index()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);

            var List = JsonConvert.DeserializeObject<List<Topic>>(getJsonRepons);

            return View(List);
        }
        public ActionResult Edit(int id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlFindTopicById + id, null);
            Topic List = JsonConvert.DeserializeObject<Topic>(getJsonRepons);

            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            ViewBag.listtopic = JsonConvert.DeserializeObject<List<Topic>>(getJsonAllTopicRepons);
            return View(List);
        }

        [HttpPost]
        public ActionResult Edit(Topic topic)
        {
            JObject topicJson = new JObject
            {
                { "Id", topic.Id },
                { "Name", topic.Name },
                { "Slug", topic.Slug },
                { "Orders", topic.Orders },
                { "Metakey", topic.Metakey },
                { "Metadesc", topic.Metadesc },
                { "Created_at", topic.Created_at.ToString("yyyy-MM-ddTHH:mm:ss") },
                { "Created_by", topic.Created_by },
                { "Updated_at", topic.Updated_at.ToString("yyyy-MM-ddTHH:mm:ss")  },
                { "Updated_by", topic.Created_by},
            };

            string EditResult = SendRequest.sendRequestPOSTwithJsonContent(ApiUrl.urlEditTopic, topicJson.ToString());
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(EditResult);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");

            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }

            return RedirectToAction("index");
        }

        public ActionResult Create()
        {
            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            ViewBag.listtopic = JsonConvert.DeserializeObject<List<Topic>>(getJsonAllTopicRepons);
            return View();
        }
        [HttpPost]
        public ActionResult Create(Topic topic)
        {
            JObject topicJson = new JObject
            {
                { "Id", 0 },
                { "Name", topic.Name },
                { "Slug", "" },
                { "Orders", topic.Orders },
                { "Metakey", topic.Metakey },
                { "Metadesc", topic.Metadesc },
                { "Created_at", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                { "Created_by", topic.Created_by },
                { "Updated_at", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")  },
                { "Updated_by", topic.Created_by},
            };
            string EditResult = SendRequest.sendRequestPOSTwithJsonContent(ApiUrl.urlCreateTopic, topicJson.ToString());
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(EditResult);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");

            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult Status(int Id, int Status)
        {

            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlChangeStatus + "?Id=" + Id + "&Status=" + Status, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");

            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult DeTrash(int Id)
        {

            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDeTrash+"?Id="+ Id, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult reTrash(int Id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlReTrash + "?Id=" + Id, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult delete(int Id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDelete + "?Id=" + Id, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");

            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult Trash()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopicTrash, null);
            var List = JsonConvert.DeserializeObject<List<Topic>>(getJsonRepons); return View(List);
        }
    }
}