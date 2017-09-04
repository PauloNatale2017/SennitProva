using Microsoft.Owin.Security.OAuth;
using Sennit.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Sennit.WEB
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        private readonly RepositoryGeneric _rep = new RepositoryGeneric();

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            
            context.Validated();
            return Task.FromResult<object>(null);
        }

        //public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        //{
        //    if (context.OwinContext.Request.Method == "GET" && context.IsTokenEndpoint)
        //    {
        //        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET" });
        //        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "accept", "authorization", "content-type" });
        //        context.OwinContext.Response.StatusCode = 200;
        //        context.RequestCompleted();
        //        return Task.FromResult<object>(null);
        //    }
        //    return base.MatchEndpoint(context);
        //}

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var Usuario = _rep._LoginRepository.GetAll().FirstOrDefault(x => x.User == context.UserName &&
                                                                             x.Password == context.Password); 

            if(Usuario == null)
            {
                context.SetError("invalid_grant",
                    "Usuario não encontrado ou senha incorreta...");
            }

            //emitindo o tokens com as informaçoes do usuario
            var idendidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(idendidadeUsuario);

        }
    }
}







