using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHangOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProductCategory",
                url: "san-pham",
                defaults: new { controller = "Products", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );
     

            routes.MapRoute(
              name: "Contact",
              url: "lien-he",
              defaults: new { controller = "Contact", action = "Index", Alias = UrlParameter.Optional },
              namespaces: new[] { "WebBanHangOnline.Controllers" }
          );
            routes.MapRoute(
            name: "Products",
            url: "danh-muc-san-pham/{alias}-{id}",
            defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
            namespaces: new[] { "WebBanHangOnline.Controllers" }
        );
            routes.MapRoute(
          name: "detailProducts",
          url: "chi-tiet/{alias}-{id}",
          defaults: new { controller = "Products", action = "Detail", id = UrlParameter.Optional },
          namespaces: new[] { "WebBanHangOnline.Controllers" }
      );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );

        }
    }
}
