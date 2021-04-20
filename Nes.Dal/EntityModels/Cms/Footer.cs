using Nes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nes.Dal.EntityModels
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "FooterID", ResourceType = typeof(NesResource))]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "AdminFunctionIdRegexMsg")]
        public string ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FooterTitleRequired")]
        [Display(Name = "FooterTitle", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }

        [Display(Name = "FooterContent", ResourceType = typeof(Resources.NesResource))]
        public string ContentHtml { get; set; }

        [Display(Name = "FooterLanguageCode", ResourceType = typeof(NesResource))]
        public string LanguageCode { get; set; }

        [Display(Name = "FooterStatus", ResourceType = typeof(Resources.NesResource))]
        public bool Status { get; set; }
    }
}
