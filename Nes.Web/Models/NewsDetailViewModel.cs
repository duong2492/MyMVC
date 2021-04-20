using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nes.Web.Models
{
    public class NewsDetailViewModel
    {
        public IEnumerable<News> HotNewses { set; get; }
        public IEnumerable<News> RelatedNewses { set; get; }
        public News NewsDetail { set; get; }
        public Category Category { set; get; }
        public IEnumerable<Tag> Tags { set; get; }
    }
}