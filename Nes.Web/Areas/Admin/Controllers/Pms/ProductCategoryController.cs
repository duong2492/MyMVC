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
using System.Web.UI;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductCategoryController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            return View(GetProductCategoryViewModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            BindingParentDropDown();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        productCategory.CreatedDate = DateTime.Now;
                        productCategory.CreatedBy = User.Identity.Name;
                        if (productCategory.ParentID != null && productCategory.ParentID.Equals(0))
                            productCategory.ParentID = null;
                        productCategory.MetaTitle = StringExtensions.ToUnsignString(productCategory.Title);
                        productCategory.LanguageCode = CultureName;
                        unitOfWork.GetRepository<ProductCategory>().Create(productCategory);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminCreateRecordSuccess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            BindingParentDropDown();
            return View(productCategory);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(long id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            ProductCategory productCategory = null;
            try
            {
                productCategory = unitOfWork.GetRepository<ProductCategory>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            BindingParentDropDown(productCategory.ParentID);
            return View(productCategory);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        productCategory.UpdatedDate = DateTime.Now;
                        productCategory.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<ProductCategory>().Update(productCategory);
                        unitOfWork.Save();

                        this.SetNotification(Nes.Resources.NesResource.AdminEditRecordSucess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);

            }
            BindingParentDropDown(productCategory.ParentID);
            return View(productCategory);
        }
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
                        unitOfWork.GetRepository<Product>().Delete(x => x.CategoryID == id);
                        unitOfWork.GetRepository<ProductCategory>().Delete(id);
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
        private void BindingParentDropDown(long? selectedId = null)
        {
            var items = GetProductCategoryViewModel();
            ViewBag.Parents = new SelectList(items, "ID", "Title", selectedId);
        }
        private List<ProductCategoryViewModel> GetProductCategoryViewModel()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<ProductCategoryViewModel> items = new List<ProductCategoryViewModel>();

            //get all of them from DB
            IEnumerable<ProductCategory> allProductCategorys = unitOfWork.GetRepository<ProductCategory>().Filter(x => x.LanguageCode.Equals(CultureName)).OrderBy(x => x.Order).ToList();
            //get parent categories
            IEnumerable<ProductCategory> parentProductCategorys = allProductCategorys.Where(c => c.ParentID == null);

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
