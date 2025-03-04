using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PagedCategory",
                url: "{category}/Page{page}",
                defaults: new { controller = "Gadget", action = "List" },
                constraints: new { page = @"\d+" } // Ensures page is a number
            );

            routes.MapRoute(
                name: "Category",
                url: "{category}",
                defaults: new { controller = "Gadget", action = "List", page = 1 }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Gadget", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
