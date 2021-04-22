using log4net;
using Nes.Common;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using Nes.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<Product>().Filter(x => x.LanguageCode.Equals(CultureName)).OrderByDescending(x => x.CreatedDate);
            return View(model.ToList());
        }

        //
        // GET: /Admin/Product/Create
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            BindingProductCateDropDown();
            return View();
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            try
            {
                product.CreatedDate = DateTime.Now;
                product.CreatedBy = User.Identity.Name;
                product.LanguageCode = CultureName;
                if (ModelState.IsValid)
                {
                    product.MetaTitle = StringExtensions.ToUnsignString(product.Title);
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        unitOfWork.GetRepository<Product>().Create(product);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminCreateRecordSuccess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
                ModelState.AddModelError("", "Hệ thống có lỗi, vui lòng liên hệ admin");

            }
            BindingProductCateDropDown();
            return View(product);
        }

        //
        // GET: /Admin/Product/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(long id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Product product = null;
            try
            {
                product = unitOfWork.GetRepository<Product>().GetById(id);
               
                
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            BindingProductCateDropDown(product.CategoryID);
            return View(product);
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        product.UpdatedDate = DateTime.Now;  
                        product.UpdatedBy = User.Identity.Name;
                        product.MetaTitle = StringExtensions.ToUnsignString(product.MetaTitle);
                        unitOfWork.GetRepository<Product>().Update(product);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminEditRecordSucess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                   // this.SetNotification(Nes.Resources.NesResource.AdminEditRecordFailed, NotificationEnumeration.Error, true);
                   // ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hệ thống có lỗi, vui lòng liên hệ admin   ");  
                logger.Error(ex);
                HandleException(ex);
            }
            BindingProductCateDropDown(product.CategoryID);
            return View(product);
        }


        //
        // POST: /Admin/Product/Delete/5

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {

                        unitOfWork.GetRepository<Product>().Delete(id);
                        unitOfWork.Save();

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return RedirectToAction("Index");
        }
        #region Private Method
        private void BindingProductCateDropDown(long? selectedValue=null)
        {
            var items = GetProductCategoryViewModel();
            ViewBag.Category = new SelectList(items, "ID", "Title",selectedValue);
        }
        private List<ProductCategoryViewModel> GetProductCategoryViewModel()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<ProductCategoryViewModel> items = new List<ProductCategoryViewModel>();

            //get all of them from DB
            IEnumerable<ProductCategory> allProductCategorys = unitOfWork.GetRepository<ProductCategory>().All().ToList();
            //get parent categories
            IEnumerable<ProductCategory> parentProductCategorys = allProductCategorys.Where(c => c.ParentID == null).OrderBy(c => c.Order);

            foreach (var cat in parentProductCategorys)
            {
                //add the parent ProductCategory to the item list
                items.Add(new ProductCategoryViewModel
                {
                    ID = cat.ID,
                    Title = cat.Title,
                    Order = cat.Order,
                    Status = cat.Status,
                    CreatedDate = cat.CreatedDate
                });
                //now get all its children (separate ProductCategory in case you need recursion)
                GetSubTree(allProductCategorys.ToList(), cat, items);
            }
            return items;
        }
        private void GetSubTree(IList<ProductCategory> allCats, ProductCategory parent, IList<ProductCategoryViewModel> items)
        {
            var subCats = allCats.Where(c => c.ParentID == parent.ID);
            foreach (var cat in subCats)
            {
                //add this ProductCategory
                items.Add(new ProductCategoryViewModel
                {
                    ID = cat.ID,
                    Title = parent.Title + " >> " + cat.Title,
                    Order = cat.Order,
                    Status = cat.Status,
                    CreatedDate = cat.CreatedDate
                });
                //recursive call in case your have a hierarchy more than 1 level deep
                GetSubTree(allCats, cat, items);
            }
        }
        #endregion

    }
}
