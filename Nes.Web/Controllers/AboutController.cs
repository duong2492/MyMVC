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
    public class AboutController : BaseController
    {
        //
        // GET: /About/

        public ActionResult Index(int? page)
        {
            //paging
            int pageSize = 8;
            int initPage = 1;
            if (page != null) initPage = page.Value;
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<About> model = unitOfWork.GetRepository<About>()
                .Filter(x => x.LanguageCode.Equals(CultureName))
                .OrderByDescending(x => x.CreatedDate).ToList();
            int count = model.Count();

            if (pageSize > 0 && initPage > 0)
            {
                int start = (initPage - 1) * pageSize;
                model = model.Skip(start).Take(pageSize).ToList();
            }
            var totalPage = 0;
            var maxpage = 5;
            try
            {
                totalPage = (int)Math.Ceiling((double)count / pageSize);
            }
            catch (Exception)
            { }
            ViewBag.Page = initPage;
            ViewBag.Total = count; ViewBag.TotalPage = totalPage; ViewBag.MaxPage = maxpage;
            ViewBag.Next = initPage + 1; if (initPage == totalPage) ViewBag.Next = 1;
            ViewBag.Prev = initPage - 1; if (initPage == 1) ViewBag.Prev = totalPage;
            return View(model);
        }

        //
        // GET: /About/Details/5

        public ActionResult Details(int id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            About  news = unitOfWork.GetRepository<About>().GetById(id);
            return View(news);
        }
        [ChildActionOnly]
        public ActionResult AboutCates()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<About>()
                .Filter(x => x.LanguageCode.Equals(CultureName))
                .OrderByDescending(x => x.CreatedDate);
            ViewBag.HotProducts = unitOfWork.GetRepository<Product>()
                .Filter(x => x.UpTopHot != null)
                .OrderByDescending(x => x.UpTopHot).Take(4);
            return View(model);
        }
     
    }
}
