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

namespace Nes.Web.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        //
        // GET: /Admin/Feedback/

        public ActionResult Index()
        {
            var model = unitOfWork.GetRepository<Feedback>().All();
            return View(model);
        }

        //
        // GET: /Admin/Feedback/Details/5

        public ActionResult Details(long id)
        {
            var model = unitOfWork.GetRepository<Feedback>().Find(id);
            model.IsReaded = true;
            unitOfWork.GetRepository<Feedback>().Update(model);
            return View(model);
        }

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
                        unitOfWork.GetRepository<Feedback>().Delete(id);
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
