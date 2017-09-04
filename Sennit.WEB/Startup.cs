using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(Sennit.WEB.Startup))]

namespace Sennit.WEB
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Sennit.ServerWebApi.Startup stp = new ServerWebApi.Startup();
            stp.Configuration(app);
            //var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{api}/{webapi}/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //app.UseWebApi(config);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);            

            // configurando rotas
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            // ativando cors          

            //WebApiConfig.Register(config);
            //app.UseCors(CorsOptions.AllowAll);
            //app.UseWebApi(config);

            //AtivandoAccessTokens(app);


            // ativando configuração WebApi
            //app.UseWebApi(config);


        }

        //public void AtivandoAccessTokens(IAppBuilder app)
        //{
        //    var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
        //    {
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
        //        Provider = new ProviderDeTokensDeAcesso()
        //    };

        //    app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
        //    app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        //}
    }
}