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
    public class ContactController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: /Admin/Contact/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<Contact>().Filter(x => x.LanguageCode.Equals(CultureName)).ToList();
            return View(model.ToList());
        }

        //
        // GET: /Admin/Contact/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Contact/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Contact/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        contact.LanguageCode = CultureName;
                        unitOfWork.GetRepository<Contact>().Create(contact);
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
            return View(contact);
        }

        //
        // GET: /Admin/Contact/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        [ValidateInput(false)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Contact contact = null;
            try
            {

                contact = unitOfWork.GetRepository<Contact>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(contact);
        }

        //
        // POST: /Admin/Contact/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        contact.LanguageCode = CultureName;
                        unitOfWork.GetRepository<Contact>().Update(contact);
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
            return View(contact);
        }

        // POST: /Admin/Contact/Delete/5

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

                        unitOfWork.GetRepository<Contact>().Delete(id);
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
