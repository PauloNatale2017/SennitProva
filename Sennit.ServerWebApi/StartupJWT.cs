using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Sennit.ServerWebApi.Securit;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(Sennit.ServerWebApi.StartupJWT))]
namespace Sennit.ServerWebApi
{
    public partial class StartupJWT
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            // configurando rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{api}/{webapi}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            OAuthAuthorizationServerOptions authServerOptions = new OAuthAuthorizationServerOptions()
            {
                //Em produção se atentar que devemos usar HTTPS
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProviderJwt(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost")
            };
            app.UseOAuthAuthorizationServer(authServerOptions);
        }
    }
}
