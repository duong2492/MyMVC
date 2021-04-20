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
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Display(Name = "CategoryID", ResourceType = typeof(Resources.NesResource))]
        public long ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "CategoryNameRequired")]
        [Display(Name = "CategoryName", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }

        [Display(Name = "CategoryMetaTitle", ResourceType = typeof(Resources.NesResource))]
        public string MetaTitle { get; set; }

        [Display(Name = "CategoryImages", ResourceType = typeof(Resources.NesResource))]
        public string Images { get; set; }

        [Display(Name = "CategoryDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }


        [Display(Name = "CategoryOrder", ResourceType = typeof(Resources.NesResource))]
        public int Order { get; set; }

        [Display(Name = "CategoryParentID", ResourceType = typeof(Resources.NesResource))]
        public long? ParentID { get; set; }
        [Required]
        [Display(Name = "CategoryCreatedDate", ResourceType = typeof(NesResource))]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "CategoryCreatedBy", ResourceType = typeof(NesResource))]
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [Display(Name = "CategoryMetaKeywords", ResourceType = typeof(Resources.NesResource))]
        public string MetaKeywords { get; set; }

        [Display(Name = "CategoryMetaDescription", ResourceType = typeof(Resources.NesResource))]
        public string MetaDescription { get; set; }

        [Display(Name = "CategoryStatus", ResourceType = typeof(Resources.NesResource))]
        public bool Status { get; set; }

        [Display(Name = "CategoryIsIntroduced", ResourceType = typeof(Resources.NesResource))]
        public bool IsIntroduced { set; get; }

        [Display(Name = "CategoryLanguageCode", ResourceType = typeof(NesResource))]
        public string LanguageCode { get; set; }

    }
}
