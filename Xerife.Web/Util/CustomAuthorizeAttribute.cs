using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Xerife.Business;

namespace Xerife.Web.Util
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private UsuarioBus bus = new UsuarioBus();
        //public void OnAuthentication(AuthenticationContext filterContext)
        //{

        //}

        //public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        //{
        //    var user = filterContext.HttpContext.User;
        //    var userRegistrado = bus.IsRegistrado(user.Identity.Name.Split('\\')[1]);
        //    if (user == null || !user.Identity.IsAuthenticated || !userRegistrado)
        //    {
        //        filterContext.Result = new HttpUnauthorizedResult();
        //    }
        //}
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var user = filterContext.HttpContext.User;
            var userRegistrado = bus.IsRegistrado(user.Identity.Name.Split('\\')[1]);


            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.Controller.TempData["ErroMessage"] = "Acesso negado.";
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }

        }
    }
}