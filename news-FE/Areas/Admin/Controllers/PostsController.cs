using news_FE.consts;
using news_FE.library;
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
            List<Post> ListPost = null;
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllPostAdmin, null);
            try
            {
                ListPost = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons);
            }
            catch
            {
                var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
                Message.set_flash(objectResult.message.Message, "danger");
                return RedirectToAction("Unauthorized", "Auth");
            }
            if (ListPost != null)
            {
                return View(ListPost.OrderByDescending(m => m.ID));
            }
            else
            {
                Message.set_flash("Đã xảy ra lỗi", "danger");
                return RedirectToAction("Unauthorized", "Auth");
            }

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
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(EditResult);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("index");
            }
            else
            {

                Message.set_flash(objectResult.message.Message, "danger");

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
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(EditResult);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("index");
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            string getJsonAllTopicRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllTopic, null);
            ViewBag.listtopic = JsonConvert.DeserializeObject<List<Topic>>(getJsonAllTopicRepons);
            return View("");
        }
        public ActionResult Status(int Id, int Status)
        {

            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlChangeStatusPost + "?Id=" + Id + "&Status=" + Status, null);
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

            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDeTrashPost + "?Id=" + Id, null);
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
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlReTrashPost + "?Id=" + Id, null);
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
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlDeletePost + "?Id=" + Id, null);
            var objectResult = JsonConvert.DeserializeObject<ObjectResult<Post>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");

            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");
            }
            return RedirectToAction("Trash");
        }
        public ActionResult Trash()
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlGetAllPostTrash, null);
            var List = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons); return View(List);
        }
        public ActionResult _getAllComment(int id)
        {
            string getJsonReponsComment = SendRequest.sendRequestGET(ApiUrl.urlGetAllComment + id, null);
            var list = JsonConvert.DeserializeObject<List<Comment>>(getJsonReponsComment).Where(m => m.ParentId ==0);
            return View("getAllComment", list);
        }
        public ActionResult _getAllSubComment(int id,int parintId)
        {
            string getJsonReponsComment = SendRequest.sendRequestGET(ApiUrl.urlGetAllComment + id, null);
            var list = JsonConvert.DeserializeObject<List<Comment>>(getJsonReponsComment).Where(m=>m.ParentId !=0 && m.ParentId == parintId);
            return View("_getAllSubComment", list);
        }
        public ActionResult ChangeStatusHidenShowComment(int Id,int Status,int postId)
        {
            string getJsonRepons = SendRequest.sendRequestGET(ApiUrl.urlChangeStatusComment + "?Id=" + Id + "&Status=" + Status, null);
            var objectResult = JConvert.jsonconvert<ObjectResult<Comment>>(getJsonRepons);
            if (objectResult.code == 200)
            {
                Message.set_flash(objectResult.message.Message, "success");
                return RedirectToAction("Edit","Posts", new { @id = postId });
            }
            else
            {
                Message.set_flash(objectResult.message.Message, "danger");

            }
            return RedirectToAction("Edit", "Posts", new { @id = postId });
        }
    }
}