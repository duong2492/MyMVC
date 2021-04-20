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

namespace Nes.Web.Controllers
{
    [AllowAnonymous]
    public class BaseController : Controller
    {
        //logger
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected string CultureName = "vi";
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
            Session[SystemConsts.CULTURE] = new CultureInfo(CultureName);
            //// Attempt to read the culture cookie from Request
            //var cultureSession = "vi";
            //if (cultureSession != null)
            //    CultureName = cultureSession.ToString();
            //else
            //    CultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
            //            Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
            //            null;
            //// Validate culture name
            //CultureName = CultureHelper.GetImplementedCulture(CultureName); // This is safe

            //// Modify current thread's cultures           
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CultureName);
            //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            ////get system name from cache
            //if (!Cache.IsSet(CACHE_SYSTEM_CONFIG))
            //{
            //    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
            //    {
            //        var configList = unitOfWork.GetRepository<SystemConfig>().Find(x => x.UniqueKey == SystemConsts.SYSTEM_NAME);
            //        Cache.Set(CACHE_SYSTEM_CONFIG, configList.Value);
            //        ViewBag.SystemNameText = configList.Value;


            //    }
            //}
            //else
            //{
            //    ViewBag.SystemNameText = Cache.Get(CACHE_SYSTEM_CONFIG);
            //}
            ////binding language dropdownlist
            //var unit = new UnitOfWork(new DbContextFactory<NesDbContext>());
            //var languageList = unit.GetRepository<Language>().All().ToList();
            //var sessionLang = Session[SystemConsts.CULTURE];
            //string currentLang = sessionLang == null ? string.Empty : sessionLang.ToString();
            //ViewBag.Languages = new SelectList(languageList, "ID", "Name", currentLang);
            return base.BeginExecuteCore(callback, state);
        }
        public ActionResult ChangeCulture(string language, string returnUrl)
        {
            Session[SystemConsts.CULTURE] = new CultureInfo(language);
            return Redirect(returnUrl);
        }
    }
}
