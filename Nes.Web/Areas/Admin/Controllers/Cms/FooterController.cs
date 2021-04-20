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
    public class FooterController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: /Admin/Footer/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<Footer>().Filter(x => x.LanguageCode.Equals(CultureName)).ToList();
            return View(model.ToList());
        }

        //
        // GET: /Admin/Footer/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Footer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Footer/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Footer Footer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        Footer.LanguageCode = CultureName;
                        unitOfWork.GetRepository<Footer>().Create(Footer);
                        unitOfWork.Save();
                        SetNotification(Nes.Resources.NesResource.AdminInserRecordSuccess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                    SetNotification(Nes.Resources.NesResource.AdminInserRecordInvalid, NotificationEnumeration.Error, true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(Footer);
        }

        //
        // GET: /Admin/Footer/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Footer Footer = null;
            try
            {

                Footer = unitOfWork.GetRepository<Footer>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(Footer);
        }

        //
        // POST: /Admin/Footer/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Footer Footer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        Footer.LanguageCode = CultureName;
                        unitOfWork.GetRepository<Footer>().Update(Footer);
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
            return View(Footer);
        }

        // POST: /Admin/Footer/Delete/5

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

                        unitOfWork.GetRepository<Footer>().Delete(id);
                        unitOfWork.Save();
                        SetNotification(Nes.Resources.NesResource.AdminDeleteRecordSuccess, NotificationEnumeration.Success, true);
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
