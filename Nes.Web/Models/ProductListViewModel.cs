using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nes.Web.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> ProductList { set; get; }
        public ProductCategory Category { set; get; }
    }
}