using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Sennit.WEB
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {           
                // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{api}/{webapi}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        

        }

      
    }
}
