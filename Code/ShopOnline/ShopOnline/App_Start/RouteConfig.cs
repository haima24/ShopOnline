using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
           "NewDetail",
           "san-pham/{id}/{text}",
           new { controller = "ProductList", action = "Detail", id = UrlParameter.Optional }
           );
            routes.MapRoute(
          "NewContact",
          "lien-he",
          new { controller = "Contact", action = "Contact", id = UrlParameter.Optional }
          );
            routes.MapRoute(
          "NewSignUp",
          "dang-ky",
          new { controller = "Contact", action = "SignUpPage", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "ProductList", action = "Index", id = UrlParameter.Optional }
             );
           
        }
    }
}