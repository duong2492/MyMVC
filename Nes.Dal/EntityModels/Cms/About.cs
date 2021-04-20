using Nes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nes.Dal.EntityModels
{
    [Table("Abouts")]
    public class About
    {
        [Key]
        [Display(Name = "AboutID", ResourceType = typeof(Resources.NesResource))]
        public long ID { get; set; }

        [Display(Name = "AboutTitle", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }

        [Display(Name = "AboutMetaTitle", ResourceType = typeof(Resources.NesResource))]
        public string MetaTitle { get; set; }

        [Display(Name = "AboutImages", ResourceType = typeof(Resources.NesResource))]
        public string Images { get; set; }

        [Display(Name = "AboutDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }

        [Display(Name = "AboutContent", ResourceType = typeof(Resources.NesResource))]
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

        [Display(Name = "ContactLanguageCode", ResourceType = typeof(NesResource))]
        public string LanguageCode { get; set; }
    }
}
