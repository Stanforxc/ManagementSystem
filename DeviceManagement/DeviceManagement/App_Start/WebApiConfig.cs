using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using DeviceManagement.MesgHandle;

namespace DeviceManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //跨域
            EnableCorsAttribute cors =  new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(
                new RequestFilter() 
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
