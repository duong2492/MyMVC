using log4net;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Controllers
{
    public class FeedbackController : BaseController
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            ViewBag.Title = "Liên hệ";
            ViewBag.LienHe = "ok chưa";
            return View();
        }
        public void SetViewBag()
        {
            var model = unitOfWork.GetRepository<Contact>()
             .Get(x => x.Status && x.LanguageCode.Equals(CultureName))
             .SingleOrDefault();
            ViewBag.ContentHtml = model.ContentHtml;
        }
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            try
            {
                feedback.CreatedDate = DateTime.Now;
                feedback.IsReaded = false;
                feedback.Title = "Phản hồi của khách hàng";
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        unitOfWork.GetRepository<Feedback>().Create(feedback);
                        unitOfWork.Save();

                        TempData["Notification"] = "Gửi thông tin phản hồi thành công. Xin cảm ơn";
                        return RedirectToAction("Create", "Feedback");
                    }

                }
                else
                {
                   // ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }
                SetViewBag();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
                ModelState.AddModelError("", Nes.Resources.NesResource.ErrorSystem);
            }
            return View();
        }
    }
}
