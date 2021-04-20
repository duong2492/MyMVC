using log4net;
using Nes.Common;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using Nes.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class FunctionController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/Function/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            return View(GetFunctionViewModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            PopulateParentIDDropDownList();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Function function)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        function.CreatedDate = DateTime.Now;
                        function.CreatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<Function>().Create(function);
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
            PopulateParentIDDropDownList(function.ParentID);
            return View(function);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Function function = null;
            try
            {
                function = unitOfWork.GetRepository<Function>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            PopulateParentIDDropDownList(function.ParentID);
            return View(function);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Function function)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        function.UpdatedDate = DateTime.Now;
                        function.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<Function>().Update(function);
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
            PopulateParentIDDropDownList(function.ParentID);
            return View(function);
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {

                        unitOfWork.GetRepository<Function>().Delete(id);
                        unitOfWork.Save();

                    }
                }
                else
                {

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
        private void PopulateParentIDDropDownList(object selectedParent = null)
        {
            var items = GetFunctionViewModel();
            ViewBag.Parents = new SelectList(items, "ID", "Text", selectedParent);
        }

        private List<FunctionViewModel> GetFunctionViewModel()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<FunctionViewModel> items = new List<FunctionViewModel>();

            //get all of them from DB
            IEnumerable<Function> allFunctions = unitOfWork.GetRepository<Function>().All().OrderBy(x => x.Order).ToList();
            //get parent categories
            IEnumerable<Function> parentFunctions = allFunctions.Where(c => c.ParentID == null).OrderBy(c => c.Order);

            foreach (var cat in parentFunctions)
            {
                //add the parent category to the item list
                items.Add(new FunctionViewModel
                {
                    ID = cat.ID,
                    Text = cat.Text,
                    Order = cat.Order,
                    IsLocked = cat.IsLocked,
                    CreatedDate = cat.CreatedDate
                });
                //now get all its children (separate function in case you need recursion)
                GetSubTree(allFunctions.ToList(), cat, items);
            }
            return items;
        }
        private void GetSubTree(IList<Function> allCats, Function parent, IList<FunctionViewModel> items)
        {
            var subCats = allCats.Where(c => c.ParentID == parent.ID);
            foreach (var cat in subCats)
            {
                //add this category
                items.Add(new FunctionViewModel
                {
                    ID = cat.ID,
                    Text = parent.Text + " >> " + cat.Text,
                    Order = cat.Order,
                    IsLocked = cat.IsLocked,
                    CreatedDate = cat.CreatedDate
                });
                //recursive call in case your have a hierarchy more than 1 level deep
                GetSubTree(allCats, cat, items);
            }
        }
        #endregion
    }
}
