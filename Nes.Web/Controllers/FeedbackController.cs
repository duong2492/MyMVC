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
    public class FeedbackController : BaseController
    {
        [HttpPost]
        [ValidateInput(false)]
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
                        return RedirectToAction("Index","Contact");
                    }

                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return View(feedback);
        }
    }
}
