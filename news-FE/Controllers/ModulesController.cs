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

namespace news_FE.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        public ActionResult _PostSuggest()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllPost, null);
            var ListPost = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons)
                .Where(m=>m.Status==1).OrderBy(m=>Guid.NewGuid()).ToList().Take(2);       
            return View("_PostSuggest", ListPost);
        }
        public ActionResult _allCategory()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            var ListCate = JsonConvert.DeserializeObject<List<Topic>>(getJsonRepons).OrderBy(m => m.Id);
            return View("_allCategory", ListCate);
        }
        public ActionResult _CategoryInMenu()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            var ListCate = JsonConvert.DeserializeObject<List<Topic>>(getJsonRepons).OrderBy(m => m.Id).Take(8);
            return View("_CategoryInMenu", ListCate);
        }
        public ActionResult _getAllComment(int id, string slug)
        {
            ViewBag.slug = slug;
            string getJsonReponsComment = SendRequest.sendRequestGET(ApiUrl.urlGetAllComment + id, null);
            var list = JsonConvert.DeserializeObject<List<Comment>>(getJsonReponsComment)
                .Where(m => m.Status == 1)
                .Where(m => m.ParentId == 0).OrderByDescending(m=>m.Create_at);
            return View("_getAllComment", list);
        }
        public ActionResult _getAllSubComment(int id, int parintId,string slug)
        {
            ViewBag.slug = slug;
            ViewBag.parintId = parintId;
            string getJsonReponsComment = SendRequest.sendRequestGET(ApiUrl.urlGetAllComment + id, null);
            var list = JsonConvert.DeserializeObject<List<Comment>>(getJsonReponsComment)
                .Where(m => m.ParentId != 0 && m.ParentId == parintId)
                .Where(m=>m.Status==1)
                .OrderByDescending(m=>m.Create_at);
            return View("_getAllSubComment", list);
        }
        [HttpPost]
        public ActionResult sendCommentSub(Comment comment, string slug)
        {
            JObject topicJson = new JObject
            {
                { "Id", 0 },
                { "PostId", comment.PostId },
                { "parentId", comment.ParentId },
                { "commentDetail", comment.CommentDetail },
                { "name", comment.Name },
                { "Star", 3 },
                { "Create_at", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") },
                { "Status", 1 },
            };
            // viết thêm đoạn này
            string EditResult = SendRequest.sendRequestPOSTwithJsonContent(ApiUrl.urlCreateComment, topicJson.ToString());
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Comment>>(EditResult);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("PostDetail", "Site", new { slug = slug });

        }
    }
}