using System.Web.Http;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "Admin_Api",
                routeTemplate: "admin/api/{controller}/{id}",
                defaults: new { area = AreaName, id = RouteParameter.Optional }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Nes.Web.Areas.Admin.Controllers" }
            );
        }
    }
}
