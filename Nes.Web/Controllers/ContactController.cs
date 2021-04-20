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
            var model = unitOfWork.GetRepository<Contact>()
                .Get(x => x.Status && x.LanguageCode.Equals(CultureName))
                .SingleOrDefault();
            return View(model);
        }

    }
}
