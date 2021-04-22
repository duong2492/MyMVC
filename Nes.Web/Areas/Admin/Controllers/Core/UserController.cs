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
    public class UserController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/User/
        //[OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<User>().Filter(x => x.IsDeleted == false);
            return View(model.ToList());
        }


        //
        // GET: /Admin/User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = User.Identity.Name;
                user.UpdatedDate = DateTime.Now;
                user.UpdatedBy = User.Identity.Name;
                user.LastChangePassword = DateTime.Now;
                user.LastLoginDate = DateTime.Now;

                if (!string.IsNullOrEmpty(user.Password))
                    user.Password = SecurityHelper.MD5Hash(user.Password);
                if (!string.IsNullOrEmpty(user.PasswordLevel2))
                    user.PasswordLevel2 = SecurityHelper.MD5Hash(user.PasswordLevel2);
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        var countUser = unitOfWork.GetRepository<User>().Find(x => x.UserName == user.UserName);
                        if (countUser == null)
                        {

                            unitOfWork.GetRepository<User>().Create(user);
                            unitOfWork.Save();
                            this.SetNotification(Nes.Resources.NesResource.AdminCreateRecordSuccess, NotificationEnumeration.Success, true);
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ModelState.AddModelError("", "Tài khoản " + user.UserName + " đã tồn tại, vui lòng tạo lại");
                        }
                    }
                }
                else
                {
                    //var errors = ModelState.Select(x => x.Value.Errors)
                    //     .Where(y => y.Count > 0)
                    //     .ToList();
                    //ModelState.AddModelError("", Nes.Resources.NesResource.ErrorValidForm);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
                ModelState.AddModelError("", "Hệ thống có lỗi, vui lòng liên hệ admin");
            }
            return View(user);
        }

        //
        // GET: /Admin/User/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            User user = null;
            try
            {
                user = unitOfWork.GetRepository<User>().GetById(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(user);
        }

        //
        // POST: /Admin/User/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        user.UpdatedDate = DateTime.Now;
                        user.UpdatedBy = User.Identity.Name;
                        unitOfWork.GetRepository<User>().Update(user);
                        unitOfWork.Save();

                        this.SetNotification(Nes.Resources.NesResource.AdminEditRecordSucess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorValidForm);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }
            return View(user);
        }

        //
        // GET: /Admin/User/Delete/5
        [HttpDelete]
        public ActionResult Delete(string id)
        {

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    if (ModelState.IsValid)
                    {
                        using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                        {
                            var user = unitOfWork.GetRepository<User>().Find(x => x.UserName == id);
                            user.IsDeleted = true;
                            unitOfWork.GetRepository<User>().Update(user);
                            unitOfWork.Save();
                            this.SetNotification("Xóa bản ghi thành công", NotificationEnumeration.Success, true);
                        }
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
