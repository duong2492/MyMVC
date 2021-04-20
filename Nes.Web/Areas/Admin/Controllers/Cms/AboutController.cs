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
    public class AboutController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<About>()
                .Filter(x => x.LanguageCode.Equals(CultureName))
                .OrderByDescending(x => x.CreatedDate);
            return View(model.ToList());
        }

        //
        // GET: /Admin/News/Create
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/News/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(About news)
        {
            try
            {
                news.CreatedDate = DateTime.Now;
                news.CreatedBy = User.Identity.Name;
                news.MetaTitle = StringExtensions.ToUnsignString(news.Title);
                news.LanguageCode = CultureName;
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        unitOfWork.GetRepository<About>().Create(news);
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
            return View(news);
        }

        //
        // GET: /Admin/News/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(long id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            About news = null;
            try
            {
                news = unitOfWork.GetRepository<About>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }

            return View(news);
        }

        //
        // POST: /Admin/News/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(About news)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        news.UpdatedDate = DateTime.Now;
                        news.UpdatedBy = User.Identity.Name;
                        news.MetaTitle = StringExtensions.ToUnsignString(news.Title);
                        unitOfWork.GetRepository<About>().Update(news);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminEditRecordSucess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    this.SetNotification(Nes.Resources.NesResource.AdminEditRecordFailed, NotificationEnumeration.Error, true);
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);

            }
            return View(news);
        }


        //
        // POST: /Admin/News/Delete/5

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

                        unitOfWork.GetRepository<About>().Delete(id);
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
