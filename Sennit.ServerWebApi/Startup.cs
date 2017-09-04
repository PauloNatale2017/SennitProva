using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Sennit.ServerWebApi.Securit;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(Sennit.ServerWebApi.Startup))]
namespace Sennit.ServerWebApi
{


    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClienteId { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            // configuracao WebApi
            var config = new HttpConfiguration();
            // configurando rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{api}/{webapi}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            PublicClienteId = "Self";

            #region teste
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // ativando cors
            //app.UseCors(CorsOptions.AllowAll);

            //// ativando tokens de acesso
            //AtivandoAccessTokens(app);
            // Web API routes
            //config.MapHttpAttributeRoutes();
            // ativando configuração WebApi

            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaulAuthenticationTypes.ExternalCookie);
            //app.UseResourceAuthorization(new AuthorizationManager());
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "Cookies"
            //});

            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/token"),
            //    Provider = new ProviderDeTokensDeAcesso(PublicClienteId),
            //    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(6),
            //    AllowInsecureHttp = true,
            //    AuthenticationMode = AuthenticationMode.Active
            //};
            #endregion

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ProviderDeTokensDeAcesso(PublicClienteId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(3),
                // Note: Remove the following line before you deploy to production:
                AllowInsecureHttp = true
            };           
                       
            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseWebApi(config);

        }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        //private void AtivandoAccessTokens(IAppBuilder app)
        //{
        //    var PublicClientId = "self";
        //    // configurando fornecimento de tokens
        //    var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
        //    {
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/token"),
        //        AuthorizeEndpointPath = new PathString("/api/webapi/login/LoginUser"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
        //        Provider = new ProviderDeTokensDeAcesso()

        //    };

        //    // ativando o uso de access tokens            
        //    app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
        //    app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        //}
    }
}
