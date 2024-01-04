using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace somiod
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Default",
             url: "api/somiod/{applicationName}/{containerName}",
             defaults: new { applicationName = UrlParameter.Optional, containerName = UrlParameter.Optional }
         );

            routes.MapRoute(
             name: "Data",
             url: "api/somiod/{applicationName}/{containerName}/data/{dataName}",
             defaults: new { applicationName = UrlParameter.Optional, 
                 containerName = UrlParameter.Optional,
                 dataName = UrlParameter.Optional }
        );

            routes.MapRoute(
             name: "Subscription",
             url: "api/somiod/{applicationName}/{containerName}/sub/{subName}",
             defaults: new { applicationName = UrlParameter.Optional,
                 containerName = UrlParameter.Optional,
                 subName = UrlParameter.Optional }
        );

        }
    }
}

