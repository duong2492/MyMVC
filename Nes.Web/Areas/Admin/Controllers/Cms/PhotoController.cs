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
    public class PhotoController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        //
        // GET: /Admin/Slide/
        public ActionResult Index()
        {
            var model = unitOfWork.GetRepository<Photo>().Filter(x=>x.Status).OrderByDescending(x => x.CreatedDate);
            return View(model.ToList());
        }

     

        //
        // GET: /Admin/Slide/Create

        public ActionResult Create()
        {

            BindingAlbumDropDown();
            return View();

        }

        //
        // POST: /Admin/Slide/Create

        [HttpPost]
        public ActionResult Create(Photo photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        photo.CreatedDate = DateTime.Now;
                        photo.CreatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<Photo>().Create(photo);
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
            BindingAlbumDropDown();
            return View(photo);
        }

        //
        // GET: /Admin/Slide/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            Photo photo = null;
            try
            {
                photo = unitOfWork.GetRepository<Photo>().GetById(id);
               
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            BindingAlbumDropDown(photo.AlbumID);
            return View(photo);
        }

        //
        // POST: /Admin/Slide/Edit/5

        [HttpPost]
        public ActionResult Edit(Photo photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        unitOfWork.GetRepository<Photo>().Update(photo);
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
            BindingAlbumDropDown(photo.AlbumID);
            return View(photo);
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
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {

                        unitOfWork.GetRepository<Slide>().Delete(id);
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
        private void BindingAlbumDropDown(long? selectedID=null)
        {
            ViewBag.Albums = new SelectList(unitOfWork.GetRepository<Album>().All().ToList(), "ID", "Title",selectedID);
        }
    }
}
