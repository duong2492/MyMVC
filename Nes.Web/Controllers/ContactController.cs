using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Controllers
{
    [AllowAnonymous]
    public class ContactController : BaseController
    {
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            ViewBag.Title = "Liên hệ";
            ViewBag.LienHe = "ok chưa";
            return View();
        }
        //
        // GET: /Admin/User/Create
        public ActionResult Create()
        {
            var model = unitOfWork.GetRepository<Contact>()
              .Get(x => x.Status && x.LanguageCode.Equals(CultureName))
              .SingleOrDefault();
           // ViewBag.ContentHtml = model.ContentHtml;
            return View(model);
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
                        return RedirectToAction("Index", "Contact");
                    }

                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }
            }
            catch (Exception ex)
            {
                //  logger.Error(ex);
                HandleException(ex);
                ModelState.AddModelError("", Nes.Resources.NesResource.ErrorSystem);
            }
            return View(feedback);
        }
    }
}
