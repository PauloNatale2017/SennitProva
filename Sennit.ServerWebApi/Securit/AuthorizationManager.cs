using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Sennit.ServerWebApi.Securit
{
    public class AuthorizationManager : ResourceAuthorizationManager
    {
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            switch (context.Resource.First().Value)
            {
                case "ContactDetails":
                    return AuthorizeContactDetails(context);
                case "LogOn":
                    return AuthorizeContactDetails(context);
                default:
                    return Nok();
            }
        }

        public Task<bool> AuthorizeContactDetails(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case "Read":
                    return Eval(context.Principal.HasClaim("role", "Admin"));
                case "Write":
                    return Eval(context.Principal.HasClaim("role", "Admin"));
                case "Update":
                    return Eval(context.Principal.HasClaim("role", "Admin"));
                case "Delete":
                    return Eval(context.Principal.HasClaim("role", "Admin"));
                case "Insert":
                    return Eval(context.Principal.HasClaim("role", "Admin"));
                case "Post":
                    return Eval(context.Principal.HasClaim("role", "Admin"));
                default:
                    return Nok();

            }

        }
    }
}
