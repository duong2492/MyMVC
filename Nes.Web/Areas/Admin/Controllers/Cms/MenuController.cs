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
    public class MenuController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/Menu/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            return View(GetClientMenuViewModel());
        }
        /// <summary>
        /// Get menu top
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [AllowAnonymous]
        public ViewResult _MenuTop()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var menu = unitOfWork.GetRepository<Menu>().Filter
                (x => x.GroupID == SystemConsts.TopPosition
                && x.Language.Equals(CultureName))
                .OrderBy(x => x.Order).ToList();
            return View(menu);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            var items = GetClientMenuViewModel();
            ViewBag.ParentID = new SelectList(items, "ID", "Text");
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            ViewBag.GroupID = new SelectList(unitOfWork.GetRepository<MenuType>().All().ToList(), "ID", "Name");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            try
            {
                menu.Language = CultureName;
                menu.CreatedDate = DateTime.Now;
                menu.CreatedBy = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        unitOfWork.GetRepository<Menu>().Create(menu);
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
            PopulateGroupIDDropDownList();
            PopulateParentIDDropDownList();
            return View(menu);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Menu menu = null;
            try
            {

                menu = unitOfWork.GetRepository<Menu>().GetById(id);
                var items = GetClientMenuViewModel();
                ViewBag.ParentID = new SelectList(items, "ID", "Text", menu.ParentID);
                ViewBag.GroupID = new SelectList(unitOfWork.GetRepository<MenuType>().All().ToList(), "ID", "Name", menu.GroupID);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", Nes.Resources.NesResource.ErrorGetRecordMessage);
            }
            return View(menu);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        menu.UpdatedDate = DateTime.Now;
                        menu.UpdatedBy = User.Identity.Name; menu.Language = CultureName;
                        unitOfWork.GetRepository<Menu>().Update(menu);
                        unitOfWork.Save();
                        var items = GetClientMenuViewModel();


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
            PopulateGroupIDDropDownList(menu.GroupID);
            PopulateParentIDDropDownList(menu.ParentID);
            return View(menu);
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

                        unitOfWork.GetRepository<Menu>().Delete(id);
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
        private void PopulateParentIDDropDownList(object selectedParent = null)
        {
            var items = GetClientMenuViewModel();
            ViewBag.Parents = new SelectList(items, "ID", "Text", selectedParent);
        }
        private void PopulateGroupIDDropDownList(object selectedParent = null)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            ViewBag.Groups = new SelectList(unitOfWork.GetRepository<MenuType>().All().ToList(), "ID", "Name", selectedParent);
        }
        private List<ClientMenuViewModel> GetClientMenuViewModel()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<ClientMenuViewModel> items = new List<ClientMenuViewModel>();

            //get all of them from DB
            IEnumerable<Menu> allMenus = unitOfWork.GetRepository<Menu>().Filter(x=>x.Language==CultureName).ToList();
            //get parent categories
            IEnumerable<Menu> parentMenus = allMenus.Where(c => c.ParentID == null).OrderBy(c => c.Order);

            foreach (var cat in parentMenus)
            {
                //add the parent category to the item list
                items.Add(new ClientMenuViewModel
                {
                    ID = cat.ID,
                    Text = cat.Text,
                    Order = cat.Order,
                    IsLocked = cat.IsLocked,
                    CreatedDate = cat.CreatedDate
                });
                //now get all its children (separate Menu in case you need recursion)
                GetSubTree(allMenus.ToList(), cat, items);
            }
            return items;
        }
        private void GetSubTree(IList<Menu> allCats, Menu parent, IList<ClientMenuViewModel> items)
        {
            var subCats = allCats.Where(c => c.ParentID == parent.ID).OrderBy(x => x.Order);
            foreach (var cat in subCats)
            {
                //add this category
                items.Add(new ClientMenuViewModel
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
