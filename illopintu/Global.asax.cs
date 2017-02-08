using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace illopintu
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application.Clear();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            HttpContext.Current.Application["listaUsers"] = null;
            HttpContext.Current.Application["listaUsers"] = new List<String>();
            HttpContext.Current.Application["dibuja"] = null;
            HttpContext.Current.Application["palabra"] = null;
        }
    }
}
