using log4net;
using Nes.Common;
using Nes.Dal.EntityModels;
using Nes.Dal.Infrastructure;
using Nes.Dal.Interfaces;
using Nes.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nes.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            var model = unitOfWork.GetRepository<News>()
                .Filter(x => x.LanguageCode.Equals(CultureName))
                .OrderByDescending(x => x.CreatedDate);
            return View(model.ToList());
        }

        //
        // GET: /Admin/News/Create
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            var items = GetCategoryViewModel();
            PopulateGroupIDDropDownList();
            return View();
        }

        //
        // POST: /Admin/News/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(News news, String tags)
        {
            try
            {
                news.CreatedDate = DateTime.Now;
                news.CreatedBy = User.Identity.Name;
                news.MetaTitle = StringExtensions.ToUnsignString(news.Title);
                news.LanguageCode = CultureName;

                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        unitOfWork.GetRepository<News>().Create(news);
                        //if (!string.IsNullOrEmpty(tags))
                        //{
                        //    string[] tagArr = tags.Split(',').Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();
                        //    foreach (var item in tagArr)
                        //    {
                        //        Tag tag = new Tag();
                        //        tag.ID = StringExtensions.ToUnsignString(item);
                        //        tag.Title = item;
                        //        tag.CreatedBy = User.Identity.Name;
                        //        tag.CreatedDate = DateTime.Now;
                        //        tag.IsActived = true;
                        //        tag.IsDefault = false;
                        //        tag.IsDeleted = false;

                        //        if (unitOfWork.GetRepository<Tag>().Filter(x => x.ID.Equals(tag.ID)).Count() == 0)
                        //        {
                        //            NewsTag newsTag = new NewsTag();
                        //            newsTag.NewsID = news.ID;
                        //            newsTag.TagID = tag.ID;
                        //            unitOfWork.GetRepository<NewsTag>().Create(newsTag);
                        //            unitOfWork.GetRepository<Tag>().Create(tag);
                        //        }
                                    
                        //    }
                        //}

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
            PopulateGroupIDDropDownList();
            return View(news);
        }

        //
        // GET: /Admin/News/Edit/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(long id)
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            News news = null;
            try
            {
                news = unitOfWork.GetRepository<News>().GetById(id);
                //string sql = string.Empty;
                //sql += "select t.* from Tags t";
                //sql += "inner join NewsTags nt on nt.TagID = t.ID";
                //sql += "inner  join Newses n on nt.NewsID = n.ID";
                //sql += "where n.ID={0}";

                //var tags = unitOfWork.GetRepository<Tag>().GetWithRawSql(string.Format(sql, id));
                //StringBuilder builder = new StringBuilder();
                //foreach (var item in tags)
                //{
                //    builder.Append(item.Title + ",");
                //}
                PopulateGroupIDDropDownList(news.CategoryID);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);
            }

            return View(news);
        }

        //
        // POST: /Admin/News/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(News news)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
                    {
                        news.UpdatedDate = DateTime.Now;
                        news.UpdatedBy = User.Identity.Name;
                        news.MetaTitle = StringExtensions.ToUnsignString(news.Title);
                        unitOfWork.GetRepository<News>().Update(news);
                        unitOfWork.Save();
                        this.SetNotification(Nes.Resources.NesResource.AdminEditRecordSucess, NotificationEnumeration.Success, true);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    this.SetNotification(Nes.Resources.NesResource.AdminEditRecordFailed, NotificationEnumeration.Error, true);
                    ModelState.AddModelError("", Nes.Resources.NesResource.ErrorCreateRecordMessage);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                HandleException(ex);

            }
            PopulateGroupIDDropDownList(news.CategoryID);
            return View(news);
        }


        //
        // POST: /Admin/News/Delete/5

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

                        unitOfWork.GetRepository<News>().Delete(id);
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
        #region Private Method
        private void CreateNewsTag(NewsTag newsTag)
        {
            using(var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>()))
            {
                unitOfWork.GetRepository<NewsTag>().Create(newsTag);
                unitOfWork.Save();
            }
        }
        private void PopulateGroupIDDropDownList(object selectedParent = null)
        {
            var items = GetCategoryViewModel();
            ViewBag.Categories = new SelectList(items, "ID", "Title", selectedParent);
        }
        private List<CategoryViewModel> GetCategoryViewModel()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory<NesDbContext>());
            List<CategoryViewModel> items = new List<CategoryViewModel>();

            //get all of them from DB
            IEnumerable<Category> allCategorys = unitOfWork.GetRepository<Category>().All().ToList();
            //get parent categories
            IEnumerable<Category> parentCategorys = allCategorys.Where(c => c.ParentID == null).OrderBy(c => c.Order);

            foreach (var cat in parentCategorys)
            {
                //add the parent category to the item list
                items.Add(new CategoryViewModel
                {
                    ID = cat.ID,
                    Title = cat.Title,
                    Order = cat.Order,
                    Status = cat.Status,
                    CreatedDate = cat.CreatedDate
                });
                //now get all its children (separate Category in case you need recursion)
                GetSubTree(allCategorys.ToList(), cat, items);
            }
            return items;
        }
        private void GetSubTree(IList<Category> allCats, Category parent, IList<CategoryViewModel> items)
        {
            var subCats = allCats.Where(c => c.ParentID == parent.ID);
            foreach (var cat in subCats)
            {
                //add this category
                items.Add(new CategoryViewModel
                {
                    ID = cat.ID,
                    Title = parent.Title + " >> " + cat.Title,
                    Order = cat.Order,
                    Status = cat.Status,
                    CreatedDate = cat.CreatedDate
                });
                //recursive call in case your have a hierarchy more than 1 level deep
                GetSubTree(allCats, cat, items);
            }
        }
        #endregion
    }
}
