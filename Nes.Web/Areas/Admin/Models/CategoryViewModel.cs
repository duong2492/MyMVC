using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nes.Web.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public int Order { set; get; }
        public bool Status { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}