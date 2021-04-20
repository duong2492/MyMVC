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
    public class GroupSlideController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/GroupSlide/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<GroupSlide>().All();
            return View(model.ToList());
        }

        //
        // GET: /Admin/GroupSlide/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/GroupSlide/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/GroupSlide/Create

        [HttpPost]
        public ActionResult Create(GroupSlide groupSlide)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        groupSlide.CreatedDate = DateTime.Now;
                        groupSlide.CreatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<GroupSlide>().Create(groupSlide);
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
            return View(groupSlide);
        }

        //
        // GET: /Admin/GroupSlide/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            GroupSlide groupSlide = null;
            try
            {
                groupSlide = unitOfWork.GetRepository<GroupSlide>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(groupSlide);
        }

        //
        // POST: /Admin/GroupSlide/Edit/5

        [HttpPost]
        public ActionResult Edit(GroupSlide groupSlide)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        groupSlide.UpdatedDate = DateTime.Now;
                        groupSlide.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<GroupSlide>().Update(groupSlide);
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
            return View(groupSlide);
        }

        // POST: /Admin/GroupSlide/Delete/5

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
                        unitOfWork.GetRepository<Slide>().Delete(x => x.GroupID == id);
                        unitOfWork.GetRepository<GroupSlide>().Delete(id);
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
