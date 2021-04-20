using Nes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nes.Dal.EntityModels
{
    [Table("Newses")]
    public class News
    {
        [Key]
        [Display(Name = "NewsID", ResourceType = typeof(Resources.NesResource))]
        public long ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "NewsTitleRequired")]
        [StringLength(250, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "NewsTitleLong")]
        [Display(Name = "NewsTitle", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }

        [Display(Name = "NewsMetaTitle", ResourceType = typeof(Resources.NesResource))]
        public string MetaTitle { get; set; }

        [Display(Name = "NewsImages", ResourceType = typeof(Resources.NesResource))]
        public string Images { get; set; }

        [Display(Name = "NewsDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }

        [Display(Name = "NewsContent", ResourceType = typeof(Resources.NesResource))]
        public string ContentHtml { get; set; }

        [Display(Name = "CategoryMetaKeywords", ResourceType = typeof(Resources.NesResource))]
        public string MetaKeywords { get; set; }

        [Display(Name = "CategoryMetaDescription", ResourceType = typeof(Resources.NesResource))]
        public string MetaDescription { get; set; }

        [Display(Name = "CategoryStatus", ResourceType = typeof(Resources.NesResource))]
        public int Status { get; set; }

        [Required]
        [Display(Name = "CategoryCreatedDate", ResourceType = typeof(NesResource))]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "CategoryCreatedBy", ResourceType = typeof(NesResource))]
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [Display(Name = "NewsPublishedDate", ResourceType = typeof(NesResource))]
        public Nullable<System.DateTime> PublishedDate { get; set; }

        [Display(Name = "NewsRelatedNewses", ResourceType = typeof(Resources.NesResource))]
        public string RelatedNewses { get; set; }

        [Display(Name = "NewsCategoryID", ResourceType = typeof(Resources.NesResource))]
        public long CategoryID { get; set; }

        [Display(Name = "NewsViewCount", ResourceType = typeof(Resources.NesResource))]
        public int ViewCount { get; set; }


        [Display(Name = "NewsSource", ResourceType = typeof(Resources.NesResource))]
        public string Source { get; set; }
        [Display(Name = "NewsUpTopNew", ResourceType = typeof(NesResource))]
        public Nullable<System.DateTime> UpTopNew { get; set; }
        [Display(Name = "NewsUpTopHot", ResourceType = typeof(NesResource))]
        public Nullable<System.DateTime> UpTopHot { get; set; }

        [Display(Name = "NewsLanguageCode", ResourceType = typeof(NesResource))]
        public string LanguageCode { get; set; }
    }
}
