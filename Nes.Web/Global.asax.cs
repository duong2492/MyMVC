using Nes.Common;
using Nes.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Nes.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            ////It's important to check whether session object is ready
            //if (HttpContext.Current.Session != null)
            //{
            //    CultureInfo ci = (CultureInfo)this.Session[SystemConsts.CULTURE];
            //    //Checking first if there is no value in session 
            //    //and set default language 
            //    //this can happen for first user's request
            //    if (ci == null)
            //    {
            //        //Sets default culture to english invariant
            //        string langName = "vi";

            //        //Try to get values from Accept lang HTTP header
            //        if (HttpContext.Current.Request.UserLanguages != null &&
            //            HttpContext.Current.Request.UserLanguages.Length != 0)
            //        {
            //            //Gets accepted list 
            //            langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
            //        }
            //        ci = new CultureInfo(langName);
            //        this.Session[SystemConsts.CULTURE] = ci;
            //    }
            //    //Finally setting culture for each request
            //    Thread.CurrentThread.CurrentUICulture = ci;
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            //}
        }
        protected void Application_Error(object sender, EventArgs e)
        {
        }
    }
}