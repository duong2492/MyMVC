using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nes.Web.Models
{
    public class NewsListViewModel
    {
        public IEnumerable<News> NewsList { set; get; }
        public Category Category { set; get; }
    }
}