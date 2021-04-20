using Nes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nes.Dal.EntityModels
{
    [Table("Languages")]
    public partial class Language
    {
        [Key]
        public string ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "LanguageNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "LanguageNameLong")]
        [Display(Name = "LanguageName", ResourceType = typeof(Resources.NesResource))]
        public string Name { get; set; }
        [Display(Name = "LanguageDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        [Display(Name = "LanguageStatus", ResourceType = typeof(Resources.NesResource))]
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "LanguageIsDefault", ResourceType = typeof(Resources.NesResource))]
        public bool IsDefault { get; set; }
    }
}
