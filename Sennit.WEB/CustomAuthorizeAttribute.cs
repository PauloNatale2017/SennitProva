using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sennit.WEB
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        public enum Permissao
        {
            Pedido,
            Produto,
            Cliente,
            Carrossel,
            Carta,
            Usuario,
            Codigo,
            Ativo,
            Menu
        }

        public Permissao[] _permissoes;
        //private MeuProjetoContext context = new MeuProjetoContext();

        public CustomAuthorizeAttribute(params Permissao[] permissoes)
        {
            _permissoes = permissoes;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //int ID = int.Parse(HttpContext.Current.User.Identity.Name);  
        

            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            var isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }

            if (!_permissoes.Any()) return true;

            //var usuarioId = LoggedUserHelper.UsuarioId(httpContext.User);
            //var usuario = context.Loja_Usuarios.SingleOrDefault(u => u.ID == usuarioId);

            foreach (var permissao in _permissoes)
            {
                switch (permissao)
                {
                    case Permissao.Pedido:
                        return true;//usuario.Pedido;
                    case Permissao.Produto:
                        return true;//usuario.Produto;
                    case Permissao.Cliente:
                        return true;//usuario.Cliente;
                    case Permissao.Carrossel:
                        return true;//usuario.Carrossel;
                    case Permissao.Carta:
                        return true;//usuario.Carta;
                    case Permissao.Usuario:
                        return true;//usuario.Usuario;
                    case Permissao.Codigo:
                        return true;//usuario.Codigo;
                    case Permissao.Ativo:
                        return true;//usuario.Ativo;
                    case Permissao.Menu:
                        return true;//usuario.Menu;
                }
            }

            return false;
        }

        // Implemente abaixo pra onde a requisição vai se o usuário não estiver autorizado
        /* protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorised"
                    })
                );
        } */

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
        
            //filterContext.RequestContext.HttpContext.GetOwinContext

         

            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name))
            {
                // A sessão está nula ou vazia, não existe usuário logado.
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "Login"
                    })
                );
            }
            else
            {
                // Usuário não tem permissão.
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "NaoAutorizado"

                    })
                );
            }
        }

    }
}