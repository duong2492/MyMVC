using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nes.Web.Models
{
    public class ProductViewModel
    {
        public Product ProductDetail { set; get; }
        public IEnumerable<Product> NewProducts { set; get; }
        public ProductCategory Category { set; get; }
        public IEnumerable<Product> RelatedProducts { set; get; }
    }
}