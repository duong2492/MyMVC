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
    public class CategoryController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            return View(GetCategoryViewModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            var items = GetCategoryViewModel();
            PopulateParentIDDropDownList();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        category.CreatedDate = DateTime.Now;
                        category.CreatedBy = User.Identity.Name;
                        if (category.ParentID != null && category.ParentID.Equals(0))
                            category.ParentID = null;
                        category.MetaTitle = StringExtensions.ToUnsignString(category.Title);
                        category.LanguageCode = CultureName;
                        unitOfWork.GetRepository<Category>().Create(category);
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
            PopulateParentIDDropDownList();
            return View(category);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(long id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Category category = null;
            try
            {
                category = unitOfWork.GetRepository<Category>().GetById(id);
                var items = GetCategoryViewModel();
               
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            PopulateParentIDDropDownList(category.ParentID);
            return View(category);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        category.UpdatedDate = DateTime.Now;
                        category.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<Category>().Update(category);
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
            PopulateParentIDDropDownList(category.ParentID);
            return View(category);
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
                        unitOfWork.GetRepository<News>().Delete(x => x.CategoryID == id);
                        unitOfWork.GetRepository<Category>().Delete(id);
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
        private void PopulateParentIDDropDownList(long? selectedParent = null)
        {
            var items = GetCategoryViewModel(selectedParent);
            ViewBag.Parents = new SelectList(items, "ID", "Title", selectedParent);
        }
        private List<CategoryViewModel> GetCategoryViewModel(long? selectedParent=null)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<CategoryViewModel> items = new List<CategoryViewModel>();

            //get all of them from DB
            IEnumerable<Category> allCategorys = unitOfWork.GetRepository<Category>()
                .Filter(x => x.LanguageCode.Equals(CultureName) && x.Status)
                .OrderBy(x => x.Order).ToList();
            //get parent categories
            IEnumerable<Category> parentCategorys = allCategorys.Where(c => c.ParentID == null);

            foreach (var cat in parentCategorys)
            {
                //add the parent category to the item list
                items.Add(new CategoryViewModel
                {
                    ID = cat.ID,
                    Title = cat.Title,
                    Order = cat.Order,
                    Status = cat.Status,
                    CreatedDate = cat.CreatedDate
                });
                //now get all its children (separate Category in case you need recursion)
                GetSubTree(allCategorys.ToList(), cat, items);
            }
            return items;
        }
        private void GetSubTree(IList<Category> allCats, Category parent, IList<CategoryViewModel> items)
        {
            var subCats = allCats.Where(c => c.ParentID == parent.ID);
            foreach (var cat in subCats)
            {
                //add this category
                items.Add(new CategoryViewModel
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
