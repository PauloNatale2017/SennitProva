using Sennit.Domain.MapEntities.Entities;
using Sennit.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sennit.WEB.Controllers
{
    public class HomeController : Controller
    {
        //private readonly RepositoryGeneric _rep = new RepositoryGeneric();

        public ActionResult Index(string id)
        {
            if (id == "Admin")
            {
                return View();
            }
            return RedirectToAction("/Login");
        }

        public ActionResult Cupons()
        {            
            return View();
        }

        public ActionResult Clientes()
        {           
            return View();
        }

        [Authorize]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CadastrarCupon()
        {
            return View();
        }

        public ActionResult CUPONSCLIENTES()
        {
            return View();
        }

        //public ActionResult LogInUser()
        //{
        //    var user = User as ClaimsPrincipal;

        //    var state = Guid.NewGuid().ToString("N");
        //    var nonce = Guid.NewGuid().ToString("N");

        //    FormsAuthentication.SetAuthCookie("Paulo", false);


        //    this.SetTempCookie(state, nonce);



        //    if (User.Identity.IsAuthenticated == true)
        //    {
        //        //Linx.Web.Controllers.CallApiController call = new CallApiController();

        //        //var contUser = call.ClientCredentials();

        //        //var access = (User as ClaimsPrincipal).Claims;

        //        //var userName = access.Select(d => d.Value).ToList();

        //        //return View((User as ClaimsPrincipal).Claims);
        //    }
        //    return View();
        //}

        //public ActionResult Logout()
        //{
        //    if (User.Identity.IsAuthenticated == true)
        //    {
        //        Request.GetOwinContext().Authentication.SignOut();
        //        return Redirect("/");
        //    }

        //    return RedirectToAction("Index");
        //}

        //public ActionResult SignOut()
        //{
        //    this.Request.GetOwinContext().Authentication.SignOut();
        //    return this.Redirect("");
        //}
    }
}