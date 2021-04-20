using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Nes.Web.Models;
using Nes.Common;

namespace Nes.Web.Controllers
{
    [AllowAnonymous]
    public class ContentController : BaseController
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        //
        // GET: /Content/

        public ActionResult Index(int? page)
        {
            //paging
            int pageSize = 8;
            int initPage = 1;
            if (page != null) initPage = page.Value;
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<News> model = unitOfWork.GetRepository<News>()
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
        [ChildActionOnly]
        public ActionResult NewsCates()
        {
            var model = unitOfWork.GetRepository<Category>()
                .Filter(x => x.LanguageCode.Equals(CultureName) && x.IsIntroduced == false)
                .OrderByDescending(x => x.CreatedDate);
            ViewBag.HotProducts = unitOfWork.GetRepository<Product>()
                .Filter(x => x.UpTopHot != null)
                .OrderByDescending(x => x.UpTopHot).Take(4);
            return View(model);
        }
       
        //
        // GET: /Content/Details/5

        public ActionResult NewsByCate(long id, int? page)
        {

            var model = new NewsListViewModel();

            //paging
            int pageSize = 8;
            int initPage = 1;
            if (page != null) initPage = page.Value;

            var newsList = unitOfWork.GetRepository<News>()
                .Filter(x => x.LanguageCode.Equals(CultureName) && x.CategoryID == id)
                .OrderByDescending(x => x.CreatedDate).ToList();
            model.NewsList = newsList;
            int count = model.NewsList.Count();

            if (pageSize > 0 && initPage > 0)
            {
                int start = (initPage - 1) * pageSize;
                model.NewsList = model.NewsList.Skip(start).Take(pageSize).ToList();
            }
            var totalPage = 0;
            var maxpage = 5;
            try
            {
                totalPage = (int)Math.Ceiling((double)count / pageSize);
            }
            catch (Exception)
            { }

            model.Category = unitOfWork.GetRepository<Category>().Find(id);
            ViewBag.Page = initPage;
            ViewBag.Total = count; ViewBag.TotalPage = totalPage; ViewBag.MaxPage = maxpage;
            ViewBag.Next = initPage + 1; if (initPage == totalPage) ViewBag.Next = 1;
            ViewBag.Prev = initPage - 1; if (initPage == 1) ViewBag.Prev = totalPage;
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var model = new NewsDetailViewModel();
            var newsDetail = unitOfWork.GetRepository<News>().Find(id);
            newsDetail.ViewCount += 1;
            unitOfWork.Save();
            //get information
            model.NewsDetail = newsDetail;
            model.Category = unitOfWork.GetRepository<Category>().Find(newsDetail.CategoryID);
            model.HotNewses = unitOfWork.GetRepository<News>()
                .Filter(x => x.UpTopHot != null && x.LanguageCode.Equals(CultureName) && x.Status == StatusEnumeration.Published)
                .OrderByDescending(x => x.UpTopHot).Take(5);

            model.RelatedNewses = unitOfWork.GetRepository<News>()
                .Filter(x => x.UpTopHot != null && x.LanguageCode.Equals(CultureName) && x.Status == StatusEnumeration.Published)
                .OrderByDescending(x => x.UpTopHot).Take(5);

            model.Tags = new List<Tag>();


            return View(model);
        }

        //
        // GET: /Content/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Content/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Content/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Content/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Content/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Content/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
