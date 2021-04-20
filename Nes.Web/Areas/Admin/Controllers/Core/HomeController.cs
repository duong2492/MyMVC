using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
     [Authorize]
    public class HomeController : BaseController
    {
         private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
