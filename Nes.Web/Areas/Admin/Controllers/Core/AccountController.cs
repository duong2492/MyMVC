using log4net;
using Nes.Web.Areas.Admin.CustomAttributes;
using Nes.Web.Areas.Admin.Models;
using Nes.Web.Areas.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Nes.Web.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Admin/Account/
        [NesHandleErrorAttribute]
        [AcceptVerbs(HttpVerbs.Get)]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "");
            }
            else
            {
                if (ModelState.IsValid && new NesMembershipProvider().ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Resources.NesResource.AdminLoginMessageError);
                }
            }

            return View(model);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
