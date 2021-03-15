using news_FE.consts;
using news_FE.library;
using news_FE.models;
using news_FE.Models;
using news_FE.Request;
using news_FE.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace news_FE.Areas.Admin.Controllers
{
    public class PostsController : BaseController
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {          
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllPost, null);
            var ListPost = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons);
            return View(ListPost.OrderByDescending(m=>m.ID));           
        }
        public ActionResult Create()
        {
            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            ViewBag.listtopic = JsonConvert.DeserializeObject<List<Topic>>(getJsonAllTopicRepons);
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Post post, HttpPostedFileBase file)
        {

            file = Request.Files["img"];
            string filename = file.FileName.ToString();
            if (filename.Equals("") == false)
            {
                string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlFindTopicById + post.Topid, null);
                Topic topic = JsonConvert.DeserializeObject<Topic>(getJsonRepons);
                string slug = Mystring.ToSlug(post.Title.ToString()) + DateTime.Now.ToString("-mmss");
                string namecate = Mystring.ToStringNospace(topic.Name);
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/post"), namefilenew);
                var folder = Server.MapPath("~/public/images/post/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                post.Img = namefilenew;
            }
            else
            {
                post.Img = "please update picture";
            }
            post.Created_at = DateTime.Now;
            JObject PostJson = new JObject
            {
                { "Id", post.ID },
                { "Topid", post.Topid },
                { "Title", post.Title },
                { "Slug", "" },
                { "Detail", post.Detail },
                { "Img", post.Img },
                { "Type", post.Type },
                { "Metakey", post.Metakey },
                { "Metadesc", post.Metadesc  },
                { "Created_at", post.Created_at.ToString("yyyy-MM-ddTHH:mm:ss")},
                { "Created_by", post.Created_by},
                { "Updated_at", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")},
                { "Updated_by", post.Updated_by},
                { "Status", post.Status},
            };
            string EditResult = SendRequest.sendRequestPOSTwithJsonContent(ApiUrl.urlCreatePost, PostJson.ToString());
            CUDResult result = JsonConvert.DeserializeObject<CUDResult>(EditResult);
          
            if (result.status == 1)
            {
                Message.set_flash(result.message, "success");
            }
            else
            {
                Message.set_flash(result.message, "danger");
            }
            // call API all topic
            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            ViewBag.listtopic = JsonConvert.DeserializeObject<List<Topic>>(getJsonAllTopicRepons);
            return View();
        }
        public ActionResult Edit(int id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlFindPostById + id, null);
            Post List = JsonConvert.DeserializeObject<Post>(getJsonRepons);

            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            ViewBag.listtopic = JsonConvert.DeserializeObject<List<Topic>>(getJsonAllTopicRepons);
            return View(List);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post, HttpPostedFileBase file)
        {
            file = Request.Files["img"];
            string filename = file.FileName.ToString();          
            if (filename.Equals("") == false)
            {
                string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlFindTopicById + post.Topid, null);
                Topic topic = JsonConvert.DeserializeObject<Topic>(getJsonRepons);
                string slug = Mystring.ToSlug(post.Title.ToString())+DateTime.Now.ToString("-mmss");
                string namecate = Mystring.ToStringNospace(topic.Name);
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/post"), namefilenew);
                var folder = Server.MapPath("~/public/images/post/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                post.Img = namefilenew;
            }
            post.Updated_at = DateTime.Now;
            JObject PostJson = new JObject
            {
                { "Id", post.ID },
                { "Topid", post.Topid },
                { "Title", post.Title },
                { "Slug", "" },
                { "Detail", post.Detail },
                { "Img", post.Img },
                { "Type", post.Type },
                { "Metakey", post.Metakey },
                { "Metadesc", post.Metadesc  },
                { "Created_at", post.Created_at.ToString("yyyy-MM-ddTHH:mm:ss")},
                { "Created_by", post.Created_by},
                { "Updated_at", post.Updated_at.ToString("yyyy-MM-ddTHH:mm:ss")},
                { "Updated_by", post.Updated_by},
                { "Status", post.Status},
            };
            string EditResult = SendRequest.sendRequestPOSTwithJsonContent(ApiUrl.urlEditPost, PostJson.ToString());
            CUDResult result = JsonConvert.DeserializeObject<CUDResult>(EditResult);
            if (result.status == 1)
            {
                Message.set_flash(result.message, "success");

            }
            else
            {
                Message.set_flash(result.message, "danger");
            }
            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            ViewBag.listtopic = JsonConvert.DeserializeObject<List<Topic>>(getJsonAllTopicRepons);
            return View("");
        }
        public ActionResult Status(int Id, int Status)
        {

            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlChangeStatusPost + "?Id=" + Id + "&Status=" + Status, null);
            CUDResult result = JConvert.jsonconvert<CUDResult>(getJsonRepons);
            if (result.status == 1)
            {
                Message.set_flash(result.message, "success");

            }
            else
            {
                Message.set_flash(result.message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult DeTrash(int Id)
        {

            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDeTrashPost + "?Id=" + Id, null);
            CUDResult result = JConvert.jsonconvert<CUDResult>(getJsonRepons);
            if (result.status == 1)
            {
                Message.set_flash(result.message, "success");
            }
            else
            {
                Message.set_flash(result.message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult reTrash(int Id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlReTrashPost + "?Id=" + Id, null);
            CUDResult result = JConvert.jsonconvert<CUDResult>(getJsonRepons);
            if (result.status == 1)
            {
                Message.set_flash(result.message, "success");
            }
            else
            {
                Message.set_flash(result.message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult delete(int Id)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDeletePost + "?Id=" + Id, null);
            CUDResult result = JConvert.jsonconvert<CUDResult>(getJsonRepons);
            if (result.status == 1)
            {
                Message.set_flash(result.message, "success");
            }
            else
            {
                Message.set_flash(result.message, "danger");
            }
            return RedirectToAction("index");
        }
        public ActionResult Trash()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllPostTrash, null);
            var List = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons); return View(List);
        }
    }
}