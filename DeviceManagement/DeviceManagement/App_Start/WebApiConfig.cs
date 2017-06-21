using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace DeviceManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            /*
            config.Routes.MapHttpRoute(
                name: "DeleteApi",
                routeTemplate : "api/{controller}/{id_1}/{id_2}",
                defaults: new { id_1 = RouteParameter.Optional, id_2 = RouteParameter.Optional }
            );*/
        }
    }
}
