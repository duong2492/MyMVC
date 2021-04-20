using log4net;
using Nes.Common;
using Nes.Common.Interfaces;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        //logger
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        UnitOfWork unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
        protected string CultureName = null;
        private ICacheHelper Cache { get; set; }
        public const string CACHE_SYSTEM_CONFIG = "SystemConfig";

        public BaseController() : this(new CacheHelper()) { }

        public BaseController(ICacheHelper cacheHelper)
        {
            // TODO: Complete member initialization
            this.Cache = cacheHelper;
        }
        protected void HandleApplicationException(ApplicationException ex)
        {
            ViewBag.Exception = ex;
            logger.Error(ex);
            RedirectToAction("Index", "Error");
        }
        protected void HandleException(Exception ex)
        {
            ViewBag.Exception = ex;
            logger.Error(ex);
            RedirectToAction("Index", "Error");
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {


            // Attempt to read the culture cookie from Request
            var cultureSession = Session[SystemConsts.CULTURE];
            if (cultureSession != null)
                CultureName = cultureSession.ToString();
            else
                CultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            CultureName = CultureHelper.GetImplementedCulture(CultureName); // This is safe

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            //get system name from cache
            if (!Cache.IsSet(CACHE_SYSTEM_CONFIG))
            {
                var configList = unitOfWork.GetRepository<SystemConfig>().Find(x => x.UniqueKey == SystemConsts.SYSTEM_NAME);
                Cache.Set(CACHE_SYSTEM_CONFIG, configList.Value);
                ViewBag.SystemNameText = configList.Value;
            }
            else
            {
                ViewBag.SystemNameText = Cache.Get(CACHE_SYSTEM_CONFIG);
            }
            //binding language dropdownlist
            var languageList = unitOfWork.GetRepository<Language>().All().ToList();
            var sessionLang = Session[SystemConsts.CULTURE];
            string currentLang = sessionLang == null ? string.Empty : sessionLang.ToString();
            ViewBag.Languages = new SelectList(languageList, "ID", "Name", currentLang);
            return base.BeginExecuteCore(callback, state);
        }
        /// <summary>
        /// change culture method
        /// </summary>
        /// <param name="language"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult ChangeCulture(string language, string returnUrl)
        {
            Session[SystemConsts.CULTURE] = new CultureInfo(language);
            return Redirect(returnUrl);
        }

        /// <summary>
        /// Set notification for list
        /// </summary>
        /// <param name="message"></param>
        /// <param name="notifyType"></param>
        /// <param name="autoHideNotification"></param>
        public void SetNotification(string message, NotificationEnumeration notifyType, bool autoHideNotification = true)
        {
            this.TempData["Notification"] = message;
            this.TempData["NotificationAutoHide"] = (autoHideNotification) ? "true" : "false";

            switch (notifyType)
            {
                case NotificationEnumeration.Success:
                    this.TempData["NotificationCSS"] = "alert alert-success";
                    break;
                case NotificationEnumeration.Error:
                    this.TempData["NotificationCSS"] = "alert alert-danger";
                    break;
                case NotificationEnumeration.Warning:
                    this.TempData["NotificationCSS"] = "alert alert-warning";
                    break;
            }
        }
    }
}
