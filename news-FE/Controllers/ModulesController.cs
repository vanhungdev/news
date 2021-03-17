using news_FE.consts;
using news_FE.Models;
using news_FE.Request;
using Newtonsoft.Json;
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
            var ListPost = JsonConvert.DeserializeObject<List<Post>>(getJsonRepons).OrderBy(m=>Guid.NewGuid()).ToList().Take(2);       
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
        public ActionResult _getAllComment(int id)
        {
            string getJsonReponsComment = SendRequest.sendRequestGET(ApiUrl.urlGetAllComment + id, null);
            var list = JsonConvert.DeserializeObject<List<Comment>>(getJsonReponsComment).Where(m => m.ParentId == 0).OrderByDescending(m=>m.Create_at);
            return View("_getAllComment", list);
        }
        public ActionResult _getAllSubComment(int id, int parintId)
        {
            string getJsonReponsComment = SendRequest.sendRequestGET(ApiUrl.urlGetAllComment + id, null);
            var list = JsonConvert.DeserializeObject<List<Comment>>(getJsonReponsComment).Where(m => m.ParentId != 0 && m.ParentId == parintId);
            return View("_getAllSubComment", list);
        }
    }
}