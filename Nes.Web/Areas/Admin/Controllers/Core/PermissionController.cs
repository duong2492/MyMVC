using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using Nes.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PermissionController :
        BaseController
    {
        //
        // GET: /Admin/Permission/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(string groupId)
        {
            return View(GetPermissionInitialModel(groupId));

        }
        [AcceptVerbs(HttpVerbs.Post)]
        private PermissionViewModel GetPermissionInitialModel(string groupId)
        {
            //setup properties
            var model = new PermissionViewModel();

            var selectedFunctions = new List<Function>();
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            model.UserGroup = unitOfWork.GetRepository<UserGroup>().GetById(groupId);
            //setup a view model
            model.AvailableFunctions = unitOfWork.GetRepository<Function>().All().ToList();
            model.SelectedFunctions = selectedFunctions;

            var selectedRoles = new List<Role>();

            model.AvailableRoles = unitOfWork.GetRepository<Role>().All().ToList();
            model.SelectedRoles = selectedRoles;
            return model;
        }
        private PermissionViewModel GetPermissionViewModel(string groupId, PostedFunctionIds postedFunctions, PostedRoleIds postedRoles)
        {
            // setup properties
            var model = new PermissionViewModel();
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            model.UserGroup = unitOfWork.GetRepository<UserGroup>().GetById(groupId);

            #region setup function
            var selectedFunctions = new List<Function>();
            var postedFunctionIds = new string[0];
            if (postedFunctions == null) postedFunctions = new PostedFunctionIds();

            // if a view model array of posted fruits ids exists
            // and is not empty,save selected ids
            if (postedFunctions.FunctionIds != null && postedFunctions.FunctionIds.Any())
            {
                postedFunctionIds = postedFunctions.FunctionIds;
            }

            // if there are any selected ids saved, create a list of fruits
            if (postedFunctionIds.Any())
            {
                selectedFunctions = unitOfWork.GetRepository<Function>().All()
                 .Where(x => postedFunctionIds.Any(s => x.ID.ToString().Equals(s)))
                 .ToList();
            }

            //setup a view model
            model.AvailableFunctions = unitOfWork.GetRepository<Function>().All().ToList();
            model.SelectedFunctions = selectedFunctions;
            model.PostedFunctions = postedFunctions;
            #endregion
            #region setup role
            var selectedRoles = new List<Role>();
            var postedRoleIds = new string[0];
            if (postedRoles == null) postedRoles = new PostedRoleIds();

            // if a view model array of posted fruits ids exists
            // and is not empty,save selected ids
            if (postedRoles.RoleIds != null && postedRoles.RoleIds.Any())
            {
                postedRoleIds = postedRoles.RoleIds;
            }
            // if there are any selected ids saved, create a list of fruits
            if (postedRoleIds.Any())
            {
                selectedRoles = unitOfWork.GetRepository<Role>().All()
                 .Where(x => postedRoleIds.Any(s => x.ID.ToString().Equals(s)))
                 .ToList();
            }

            //setup a view model
            model.AvailableRoles = unitOfWork.GetRepository<Role>().All().ToList();
            model.SelectedRoles = selectedRoles;
            model.PostedRoles = postedRoles;
            #endregion
            return model;
        }
        //
        // GET: /Admin/Permission/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Permission/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Permission/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Permission/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Permission/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Permission/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Permission/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
