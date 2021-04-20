using Nes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nes.Dal.EntityModels
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Display(Name = "ProductID", ResourceType = typeof(Resources.NesResource))]
        public long ID { get; set; }

        [Display(Name = "ProductCode", ResourceType = typeof(Resources.NesResource))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "ProductTitleRequired")]
        [StringLength(250, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "ProductTitleLong")]
        [Display(Name = "ProductTitle", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }

        [Display(Name = "ProductMetaTitle", ResourceType = typeof(Resources.NesResource))]
        public string MetaTitle { get; set; }

        [Display(Name = "ProductDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }

        [Display(Name = "ProductImages", ResourceType = typeof(Resources.NesResource))]
        [Column(TypeName = "xml")]
        public string Images { get; set; }

        [NotMapped]
        public XElement XImages
        {
            get { return XElement.Parse(Images); }
            set { Images = value.ToString(); }
        }


        [Display(Name = "ProductPrice", ResourceType = typeof(Resources.NesResource))]
        public decimal Price { get; set; }

        [Display(Name = "ProductMetaKeywords", ResourceType = typeof(Resources.NesResource))]
        public string MetaKeywords { get; set; }

        [Display(Name = "ProductMetaDescription", ResourceType = typeof(Resources.NesResource))]
        public string MetaDescription { get; set; }

        [Display(Name = "ProductStatus", ResourceType = typeof(Resources.NesResource))]
        public int Status { get; set; }

        [Display(Name = "ProductCreatedDate", ResourceType = typeof(NesResource))]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "ProductCreatedBy", ResourceType = typeof(NesResource))]
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [Display(Name = "ProductCategoryID", ResourceType = typeof(Resources.NesResource))]
        public long CategoryID { get; set; }


        [Display(Name = "ProductViewCount", ResourceType = typeof(Resources.NesResource))]
        public int ViewCount { get; set; }

        [Display(Name = "ProductUpTopNew", ResourceType = typeof(NesResource))]
        public Nullable<System.DateTime> UpTopNew { get; set; }
        [Display(Name = "ProductUpTopHot", ResourceType = typeof(NesResource))]
        public Nullable<System.DateTime> UpTopHot { get; set; }

        [Display(Name = "ProductDetail", ResourceType = typeof(NesResource))]
        public string Detail { get; set; }


        [Display(Name = "ProductLanguageCode", ResourceType = typeof(NesResource))]
        public string LanguageCode { get; set; }
    }
}
