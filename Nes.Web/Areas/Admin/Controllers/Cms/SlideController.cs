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
using System.Web.Caching;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        //
        // GET: /Admin/Slide/
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<Slide>().All().OrderBy(x => x.Order);
            return View(model.ToList());
        }

        //
        // GET: /Admin/Slide/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Slide/Create

        public ActionResult Create()
        {
             BindingGroupDropDown();
            return View();

        }

        //
        // POST: /Admin/Slide/Create

        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        unitOfWork.GetRepository<Slide>().Create(slide);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminCreateRecordSuccess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
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
            BindingGroupDropDown();
            return View(slide);
        }

        //
        // GET: /Admin/Slide/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Slide slide = null;
            try
            {
                slide = unitOfWork.GetRepository<Slide>().GetById(id);
               
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
             BindingGroupDropDown(slide.GroupID);
            return View(slide);
        }

        //
        // POST: /Admin/Slide/Edit/5

        [HttpPost]
        public ActionResult Edit(Slide slide)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        unitOfWork.GetRepository<Slide>().Update(slide);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminEditRecordSucess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
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
           BindingGroupDropDown(slide.GroupID);
            return View(slide);
        }

        // POST: /Admin/Slide/Delete/5

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {

                        unitOfWork.GetRepository<Slide>().Delete(id);
                        unitOfWork.Save();

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return RedirectToAction("Index");
        }
        private void BindingGroupDropDown(string selectedID=null)
        {
            ViewBag.Groups = new SelectList(unitOfWork.GetRepository<GroupSlide>().All().ToList(), "ID", "Name",selectedID);
        }
    }
}
