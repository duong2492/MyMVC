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
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "ContactID", ResourceType = typeof(NesResource))]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "AdminFunctionIdRegexMsg")]
        public string ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "ContactTitleRequired")]
        [Display(Name = "ContactTitle", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }

        [Display(Name = "ContactContent", ResourceType = typeof(Resources.NesResource))]
        
        public string ContentHtml { get; set; }

        [Display(Name = "ContactLanguageCode", ResourceType = typeof(NesResource))]
        public string LanguageCode { get; set; }

        [Display(Name = "ContactStatus", ResourceType = typeof(Resources.NesResource))]
        public bool Status { get; set; }
    }
}
