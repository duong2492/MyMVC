using log4net;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using Nes.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Nes.Web.Areas.Admin.Controllers
{
    public class NavigationController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/Navigation/
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult _MainMenu()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            //string query = "exec GetViewMenuBaseRole @username";
            //SqlParameter sqlPara = new SqlParameter("@username", User.Identity.Name);
            //var menuList = unitOfWork.GetRepository<Function>().GetWithRawSql(query, sqlPara);
            List<Function> menusource = unitOfWork.GetRepository<Function>().All().OrderBy(x => x.Order).ToList();
            List<MenuViewModel> model = CreateVM(null, menusource);  // transform it into the ViewModel
            return View(model);

        }
        public List<MenuViewModel> CreateVM(string parentid, List<Function> source)
        {
            var query = from men in source
                        where men.ParentID == parentid
                        select new MenuViewModel()
                        {
                            MenuId = men.ID,
                            Text = men.Text,
                            Link = men.Link,
                            Target = men.Target,
                            CssClass = men.CssClass,
                            Children = CreateVM(men.ID, source)
                        };
            return query.ToList();
        }
    }
}
