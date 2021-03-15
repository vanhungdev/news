using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace news_FE.consts
{
    public static class ApiUrl
    {
        /// <summary>
        /// domain
        /// </summary>
        public const string domain = "https://localhost:44313/";
        /// <summary>
        /// url get all topic method => GET
        /// </summary>
        public const string urlGetAllTopic = domain+"api/Category/getAll";
        /// <summary>
        ///  Url find topic by id method => GET
        /// </summary>
        public const string urlFindTopicById = domain+"api/Category/findById/";
        /// <summary>
        ///  Url edit  method => POST
        /// </summary>
        public const string urlEditTopic = domain+"api/Category/Edit";
        /// <summary>
        ///  Url create topic method => POST
        /// </summary>
        public const string urlCreateTopic = domain+"api/Category/Create";
        /// <summary>
        /// url change status method => GET
        /// </summary>
        public const string urlChangeStatus = domain + "api/Category/ChangeStatus";
        /// <summary>
        ///  Url delete method => GET
        /// </summary>
        public const string urlDelete = domain + "api/Category/delete/";
        /// <summary>
        ///  Url detrash category  method => GET
        /// </summary>
        public const string urlDeTrash = domain + "api/Category/DeTrash/";
        /// <summary>
        ///  Url url retrash  method => GET
        /// </summary>
        public const string urlReTrash = domain + "api/Category/ReTrash/";
        /// <summary>
        ///  Url url retrash  method => GET
        /// </summary>
        public const string urlGetAllTopicTrash = domain + "api/Category/getAllTopicTrash";
        ////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// url get all Post method => POST
        /// </summary>
        public const string urlGetAllPost = domain+"api/Posts/getAllPost";
        /// <summary>
        ///  Url find Post by id + id  method => GET
        /// </summary>
        public const string urlFindPostById = domain+"api/Posts/findPostById/";
        /// <summary>
        ///  Url find Post by slug +slug method => GET
        /// </summary>
        public const string urlFindPostyslug = domain+"api/Posts/findPostBySlug/";
        /// <summary>
        ///  Url get post by category id + category id method => GET
        /// </summary>
        public const string urlGetPostByCategoryId = domain+"api/Posts/getPostByCategoryId/";
        /// <summary>
        ///  Url create Post POST
        /// </summary>
        public const string urlCreatePost = domain+"api/Posts/createPost";
        /// <summary>
        ///  Url Edit Post POST
        /// </summary>
        public const string urlEditPost = domain+"api/Posts/EditPost";
        /// <summary>
        /// url change status method => GET
        /// </summary>
        public const string urlChangeStatusPost = domain + "api/Posts/ChangeStatus";
        /// <summary>
        ///  Url delete method => GET
        /// </summary>
        public const string urlDeletePost = domain + "api/Posts/delete/";
        /// <summary>
        ///  Url detrash category  method => GET
        /// </summary>
        public const string urlDeTrashPost = domain + "api/Posts/DeTrash/";
        /// <summary>
        ///  Url url retrash  method => GET
        /// </summary>
        public const string urlReTrashPost = domain + "api/Posts/ReTrash/";
        /// <summary>
        ///  Url url retrash  method => GET
        /// </summary>
        public const string urlGetAllPostTrash = domain + "api/Posts/getAllTopicTrash";



        /// <summary>
        ///  Url login POST
        /// </summary>
        public const string urlAuthen = domain+ "api/Users/Authenticate";

        /// <summary>
        ///  Url getAll User method => GET
        /// </summary>
        public const string urlGetAllUser = domain+"api/Users/getAllUser";

    }
}