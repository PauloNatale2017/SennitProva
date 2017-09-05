using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Sennit.ServerWebApi.Securit;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using System.Linq;
using Microsoft.Owin.Security.DataHandler.Encoder;

[assembly: OwinStartup(typeof(Sennit.ServerWebApi.Startup))]
namespace Sennit.ServerWebApi
{


    public class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            //// configuracao WebApi
            //var config = new HttpConfiguration();
            //// configurando rotas
            //config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{api}/{webapi}/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //PublicClienteId = "Self";

            //#region teste
            ////config.SuppressDefaultHostAuthentication();
            ////config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //// ativando cors
            ////app.UseCors(CorsOptions.AllowAll);

            ////// ativando tokens de acesso
            ////AtivandoAccessTokens(app);
            //// Web API routes
            ////config.MapHttpAttributeRoutes();
            //// ativando configuração WebApi

            ////app.UseCookieAuthentication(new CookieAuthenticationOptions());
            ////app.UseExternalSignInCookie(DefaulAuthenticationTypes.ExternalCookie);
            ////app.UseResourceAuthorization(new AuthorizationManager());
            ////app.UseCookieAuthentication(new CookieAuthenticationOptions
            ////{
            ////    AuthenticationType = "Cookies"
            ////});

            ////OAuthOptions = new OAuthAuthorizationServerOptions
            ////{
            ////    TokenEndpointPath = new PathString("/token"),
            ////    Provider = new ProviderDeTokensDeAcesso(PublicClienteId),
            ////    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            ////    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(6),
            ////    AllowInsecureHttp = true,
            ////    AuthenticationMode = AuthenticationMode.Active
            ////};
            //#endregion

            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/Token"),
            //    Provider = new ProviderDeTokensDeAcesso(PublicClienteId),
            //    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(3),
            //    // Note: Remove the following line before you deploy to production:
            //    AllowInsecureHttp = true
            //};           

            //app.UseOAuthBearerTokens(OAuthOptions);
            //

            var config = new HttpConfiguration();
            // configurando rotas
           

            OAuthAuthorizationServerOptions authServerOptions = new OAuthAuthorizationServerOptions()
            {
                //Em produção se atentar que devemos usar HTTPS
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
                Provider = new CustomOAuthProviderJwt(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost:54992/api/webapi/")
            };

            app.UseOAuthAuthorizationServer(authServerOptions);

            var issuer = "http://localhost:54992/api/webapi/";
            var audience = WebApplicationAccess.WebApplicationAccessList.Select(x => x.Value.ClientId).AsEnumerable();
            var secretsSymmetricKey = (from x in WebApplicationAccess.WebApplicationAccessList select new SymmetricKeyIssuerSecurityTokenProvider(issuer,TextEncodings.Base64Url.Decode(x.Value.SecretKey))).ToArray();
                app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = audience,
                    IssuerSecurityTokenProviders = secretsSymmetricKey
                });

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{api}/{webapi}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);

        }


    }

      
}
