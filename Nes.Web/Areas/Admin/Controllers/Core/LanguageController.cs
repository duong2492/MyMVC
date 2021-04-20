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
    public class LanguageController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: /Admin/Language/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<Language>().All();
            return View(model.ToList());
        }

        //
        // GET: /Admin/Language/Create
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Language/Create

        [HttpPost]
        public ActionResult Create(Language language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        language.CreatedDate = DateTime.Now;
                        language.CreatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<Language>().Create(language);
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
            return View(language);
        }

        //
        // GET: /Admin/Language/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Language language = null;
            try
            {
                language = unitOfWork.GetRepository<Language>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(language);
        }

        //
        // POST: /Admin/Language/Edit/5

        [HttpPost]
        public ActionResult Edit(Language language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        language.UpdatedDate = DateTime.Now;
                        language.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<Language>().Update(language);
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
            return View(language);
        }


        //
        // POST: /Admin/Language/Delete/5

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

                        unitOfWork.GetRepository<Language>().Delete(id);
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
