using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PencilBox
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Pure",
                url: "",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "tagName",
                url: "{tagName}",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "{tag}/{category}",
                defaults: new { controller = "Product", action = "Category" }
            );

            routes.MapRoute(
                name: "Default",
                url: "ctrl/{controller}/{action}/{id}",
                defaults: new { controller = "Product", id = UrlParameter.Optional }
            );
        }
    }
}