using Nes.Common;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        UnitOfWork unit = new UnitOfWork(new DbContextFactory<NesDbContext>());
        public ActionResult Intro()
        {
            return View();
        }
        public ActionResult Index()
        {
            ViewBag.TopSlides = unit.GetRepository<Slide>()
                .Filter(x => x.GroupID == SystemConsts.TopPosition)
                .OrderBy(x => x.Order).ToList();

            ViewBag.HotLine = unit.GetRepository<News>()
                .Filter(x => x.UpTopHot != null && x.LanguageCode == CultureName && x.Status == StatusEnumeration.Published)
                .OrderByDescending(x => x.UpTopHot).ToList();
            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            try
            {
                ViewBag.HotProductCates = unit.GetRepository<ProductCategory>()
               .Filter(x => x.LanguageCode == CultureName && x.Status).Take(5)
               .OrderBy(x => x.Order).ToList();

                ViewBag.BottomMenu = unit.GetRepository<Menu>()
                    .Filter(x => x.GroupID == SystemConsts.BottomPosition && x.Language.Equals(CultureName))
                    .OrderBy(x => x.Order).ToList();
                var footer = unit.GetRepository<Footer>()
                    .Filter(x => x.LanguageCode == CultureName && x.Status).SingleOrDefault().ContentHtml;
                ViewBag.Footer = footer;
            }
            catch (Exception ex)
            {

                HandleException(ex);
            }


            return View();
        }

    }
}
