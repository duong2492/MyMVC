using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using Nes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Controllers
{
    [AllowAnonymous]
    public class CollectionController : BaseController
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        //
        // GET: /Collection/

        public ActionResult Index()
        {
            var model = unitOfWork.GetRepository<Album>()
                .Filter(x => x.LanguageCode == CultureName && x.Status)
                .OrderBy(x => x.Order);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult AlbumList()
        {
            var model = unitOfWork.GetRepository<Album>()
                .Filter(x => x.LanguageCode.Equals(CultureName) && x.Status)
                .OrderByDescending(x => x.CreatedDate);
            ViewBag.HotProducts = unitOfWork.GetRepository<Product>()
                .Filter(x => x.UpTopHot != null && x.LanguageCode==CultureName)
                .OrderByDescending(x => x.UpTopHot).Take(4);
            return View(model);
        }
        //
        // GET: /Collection/Details/5

        public ActionResult Album(long id)
        {
            var model = new PhotoViewModel();
            model.PhotoList = unitOfWork.GetRepository<Photo>()
                .Filter(x => x.Status && x.AlbumID==id)
                .OrderByDescending(x => x.CreatedDate);
            model.Album = unitOfWork.GetRepository<Album>().Find(id);
            return View(model);
        }

    }
}
