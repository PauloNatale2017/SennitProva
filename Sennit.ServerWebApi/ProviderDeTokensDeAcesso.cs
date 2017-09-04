using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Owin.Security.OAuth;
using Sennit.ServerWebApi.Usuarios;
using Newtonsoft.Json;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using Microsoft.Owin.Security.Cookies;
using Sennit.WEB;
using Sennit.SPA.Models;

namespace Sennit.ServerWebApi
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {

        private readonly string _publicClientId;
        public ProviderDeTokensDeAcesso(string publicClientId)
        {
             if (publicClientId == null){throw new ArgumentNullException("publicClientId"); }
             _publicClientId = publicClientId;
        }

      
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            #region TESTE
            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}

            //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
            //   OAuthDefaults.AuthenticationType);
            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //context.Validated();
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
            #endregion

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            AuthenticationProperties properties = CreateProperties("Paulo");
            
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //encontrando o usuário
            var usuario = BaseUsuarios
                   .Usuarios()
                   .FirstOrDefault(x => x.Nome == "Fulano"
                                   && x.Senha == "1234");

            // cancelando a emissão do token se o usuário não for encontrado
            if (usuario == null)
            {
                context.SetError("invalid_grant",
                    "Usuário não encontrado um senha incorreta.");
                return;
            }

            // emitindo o token com informacoes extras
            // se o usuário existe
            var identidadeUsuario = 
                new ClaimsIdentity(context.Options.AuthenticationType);

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

            foreach (var funcao in usuario.Funcoes)
                identidadeUsuario.AddClaim(new Claim(ClaimTypes.Role, funcao));


            context.Validated(identidadeUsuario);
            context.Request.Context.Authentication.SignIn(identidadeUsuario);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }


        #region TESTE

        //public override async Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        //{
        //    context.Validated();
        //    base.ValidateAuthorizeRequest(context);
        //}

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    var usuario = BaseUsuarios
        //        .Usuarios()
        //        .FirstOrDefault(x => x.Nome == context.UserName
        //                        && x.Senha == context.Password);
        //    //var user = userService.GetUser(context.UserName, context.Password);
        //    var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
        //    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nome));
        //    var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
        //    context.Validated(ticket);
        //    base.GrantResourceOwnerCredentials(context);
        //}


        //public override async Task ValidateClientAuthentication
        //    (OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();
        //}

        //public override async Task GrantResourceOwnerCredentials
        //    (OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    // encontrando o usuário
        //    var usuario = BaseUsuarios
        //        .Usuarios()
        //        .FirstOrDefault(x => x.Nome == context.UserName
        //                        && x.Senha == context.Password);

        //    // cancelando a emissão do token se o usuário não for encontrado
        //    if (usuario == null)
        //    {
        //        context.SetError("invalid_grant",
        //            "Usuário não encontrado um senha incorreta.");
        //        return;
        //    }

        //    // emitindo o token com informacoes extras
        //    // se o usuário existe
        //    var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);

        //    foreach (var funcao in usuario.Funcoes)
        //        identidadeUsuario.AddClaim(new Claim(ClaimTypes.Role, funcao));

        //    context.Validated(identidadeUsuario);
        //}

        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    // repassando propriedades do token, mas de forma aberta
        //    foreach (var item in context.Properties.Dictionary)
        //    {
        //        context.AdditionalResponseParameters.Add(item.Key, item.Value);
        //    }

        //    // repassando claims que estão no token, mas de forma aberta
        //    var claims = context.Identity.Claims
        //        .GroupBy(x => x.Type)
        //        .Select(y => new { Claim = y.Key, Values = y.Select(z => z.Value).ToArray() });

        //    foreach (var claim in claims)
        //    {
        //        context.AdditionalResponseParameters.Add(claim.Claim, JsonConvert.SerializeObject(claim.Values));
        //    }

        //    // repassando informacões fixas
        //    context.AdditionalResponseParameters.Add("info1", "valor");
        //    context.AdditionalResponseParameters.Add("info2", 1);

        //    return base.TokenEndpoint(context);
        //}
        #endregion


    }
}

