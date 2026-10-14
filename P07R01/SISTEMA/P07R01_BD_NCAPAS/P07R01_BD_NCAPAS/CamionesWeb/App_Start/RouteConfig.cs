using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.AspNet.FriendlyUrls;
using Microsoft.AspNet.FriendlyUrls.Resolvers;

namespace CamionesWeb
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            //se usa un resolver propio para que en celulares NO cambie a Site.Mobile.Master
            routes.EnableFriendlyUrls(settings, new SinMasterMovilResolver());
        }
    }

    //Por defecto FriendlyUrls usa Site.Mobile.Master (sin estilos) cuando detecta un celular.
    //camionesWeb.css ya es responsivo, asi que siempre se usa Site.Master.
    public class SinMasterMovilResolver : WebFormsFriendlyUrlResolver
    {
        protected override bool TrySetMobileMasterPage(HttpContextBase httpContext, Page page, string mobileSuffix)
        {
            return false;
        }
    }
}
