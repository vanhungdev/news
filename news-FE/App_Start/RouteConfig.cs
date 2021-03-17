using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace news_FE
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "get all post by category",
               url: "{slug}",
               defaults: new { controller = "Site", action = "allPostBySlugCategory", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "PostDetail",
                url: "chi-tiet-bai-viet/{slug}",
                defaults: new { controller = "Site", action = "PostDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
