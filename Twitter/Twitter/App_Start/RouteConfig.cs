using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Twitter
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "New",
                url: "Post/New",
                defaults: new { controller = "Post", action = "New", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Create",
                url: "Post/Create",
                defaults: new { controller = "Post", action = "New", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Pagination",
                url: "Post/{id}",
                defaults: new { controller = "Post", action = "Details", id = 0}
                );

            routes.MapRoute(
                name: "Details",
                url: "Post/Details/{id}",
                defaults: new { controller = "Post", action = "Details", id = 0 }
                );

            routes.MapRoute(
             name: "defaultforposts",
                url: "Post/{action}/{id}",
              defaults: new { controller = "Post", id = UrlParameter.Optional }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Twitter", action = "StartPage", id = UrlParameter.Optional }
            );
        }
    }
}
