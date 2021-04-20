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
    public class ProductController : BaseController
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        // GET: /Product/

        public ActionResult Index(int? page)
        {
            //paging
            int pageSize = 6;
            int initPage = 1;
            if (page != null) initPage = page.Value;

            var model = unitOfWork.GetRepository<Product>()
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
        public ActionResult ProductByCate(long id,int? page)
        {

            var model = new ProductListViewModel();
            //paging
            int pageSize = 6;
            int initPage = 1;
            if (page != null) initPage = page.Value;

            var productList = unitOfWork.GetRepository<Product>()
                .Filter(x => x.LanguageCode.Equals(CultureName) && x.CategoryID == id)
                .OrderByDescending(x => x.CreatedDate);

            model.ProductList = productList;
            int count = model.ProductList.Count();

            if (pageSize > 0 && initPage > 0)
            {
                int start = (initPage - 1) * pageSize;
                model.ProductList = model.ProductList.Skip(start).Take(pageSize).ToList();
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
            model.Category = unitOfWork.GetRepository<ProductCategory>().Find(id);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult ProductCates()
        {
            var model = unitOfWork.GetRepository<ProductCategory>()
                .Filter(x => x.LanguageCode.Equals(CultureName))
                .OrderByDescending(x => x.CreatedDate);
            ViewBag.HotProducts = unitOfWork.GetRepository<Product>()
                .Filter(x => x.UpTopHot != null)
                .OrderByDescending(x => x.UpTopHot).Take(4);
            return View(model);
        }
        //
        // GET: /Product/Details/5

        public ActionResult Detail(long id)
        {
            ProductViewModel model = new ProductViewModel();
            var product = unitOfWork.GetRepository<Product>().Find(id);
            product.ViewCount += 1;
            unitOfWork.Save();
            model.ProductDetail = product;
            model.Category = unitOfWork.GetRepository<ProductCategory>().Find(product.CategoryID);
            model.NewProducts = unitOfWork.GetRepository<Product>().Get()
                .Take(4).OrderByDescending(x => x.CreatedDate);
            model.RelatedProducts = unitOfWork.GetRepository<Product>()
                .Filter(x => x.CategoryID.Equals(product.CategoryID))
                .Take(4).OrderByDescending(x => x.CreatedDate);
            return View(model);
        }

        public ActionResult Search(string s,int? page)
        {
            //paging
            int pageSize = 6;
            int initPage = 1;
            if (page != null) initPage = page.Value;
            var model = unitOfWork.GetRepository<Product>()
                .Filter(x => x.LanguageCode.Equals(CultureName) && (x.Title.Contains(s) || x.Description.Contains(s)))
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

    }
}
