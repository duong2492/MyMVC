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
    public class SystemConfigController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        //
        // GET: /Admin/SystemConfig/

        public ActionResult Index()
        {
            var model = unitOfWork.GetRepository<SystemConfig>().All();
            return View(model);
        }

        //
        // GET: /Admin/SystemConfig/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/SystemConfig/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SystemConfig/Create

        [HttpPost]
        public ActionResult Create(SystemConfig systemConfig)
        {
            try
            {
                // TODO: Add insert logic here
                unitOfWork.GetRepository<SystemConfig>().Create(systemConfig);
                unitOfWork.Save();
                SetNotification(Nes.Resources.NesResource.AdminInserRecordSuccess, NotificationEnumeration.Success, true);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/SystemConfig/Edit/5

        public ActionResult Edit(int id)
        {
            var model = unitOfWork.GetRepository<SystemConfig>().Find(id);
            return View(model);
        }

        //
        // POST: /Admin/SystemConfig/Edit/5

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
        // GET: /Admin/SystemConfig/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/SystemConfig/Delete/5

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
