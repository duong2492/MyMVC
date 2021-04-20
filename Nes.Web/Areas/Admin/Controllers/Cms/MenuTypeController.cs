using log4net;
using Nes.Common;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class MenuTypeController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/MenuType/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<MenuType>().All();
            return View(model.ToList());
        }

        //
        // GET: /Admin/MenuType/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/MenuType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/MenuType/Create

        [HttpPost]
        public ActionResult Create(MenuType MenuType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        MenuType.CreatedDate = DateTime.Now;
                        MenuType.CreatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<MenuType>().Create(MenuType);
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
            return View(MenuType);
        }

        //
        // GET: /Admin/MenuType/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            MenuType MenuType = null;
            try
            {
                MenuType = unitOfWork.GetRepository<MenuType>().GetById(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", Nes.Resources.NesResource.ErrorGetRecordMessage);
            }
            return View(MenuType);
        }

        //
        // POST: /Admin/MenuType/Edit/5

        [HttpPost]
        public ActionResult Edit(MenuType menuType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        menuType.UpdatedDate = DateTime.Now;
                        menuType.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<MenuType>().Update(menuType);
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
            return View(menuType);
        }

        // POST: /Admin/MenuType/Delete/5

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
                        unitOfWork.GetRepository<Menu>().Delete(x => x.GroupID == id);
                        unitOfWork.GetRepository<MenuType>().Delete(id);
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
    }
}
