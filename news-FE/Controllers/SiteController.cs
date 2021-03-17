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
    public class SiteController : Controller
    {
        // GET: Site
        public ActionResult PostDetail(string slug)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlFindPostyslug + slug, null);
            Post post = JsonConvert.DeserializeObject<Post>(getJsonRepons);
            return View("PostDetail", post);
        }
        // TODO
        public ActionResult allPostBySlugCategory(string slug)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlFindCategoryBySlug + slug, null);
            Topic topic = JsonConvert.DeserializeObject<Topic>(getJsonRepons);
            
            //sau khi lay dc id cua topic tu slug
            string getJsonReponAllPost = SendRequest.sendRequestGET(ApiUrl.urlGetAllPostByCategoryId +topic.Id, null);
            var listPost = JsonConvert.DeserializeObject<List<Post>>(getJsonReponAllPost);
            return View("allPostBySlugCategory", listPost);
        }
        [HttpPost]
        public ActionResult sendComment(Comment comment, string slug)
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
            return RedirectToAction("PostDetail", "Site" ,new { slug = slug});

        }
    }
}