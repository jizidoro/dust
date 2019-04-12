using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Xerife.Business;
using Xerife.Web.Mappers;

namespace Xerife.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private UsuarioBus bus = new UsuarioBus();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMappings();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs args)
        {
            if (Context.User != null)
            {
                string[] rolesArray = { bus.GetPerfilUsuario(Context.User.Identity.Name.Split('\\')[1]) };
                GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, rolesArray);
                Context.User = gp;
            }
        }

    }
}
