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
    [Table("Slides")]
    public class Slide
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuNameRequired")]
        [Display(Name = "MenuName", ResourceType = typeof(Resources.NesResource))]
        public string Name { get; set; }

        [Display(Name = "MenuDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuUrlRequired")]
        [Display(Name = "MenuUrl", ResourceType = typeof(NesResource))]
        public string Link { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuUrlRequired")]
        [Display(Name = "CategoryImages", ResourceType = typeof(NesResource))]
        public string Images { get; set; }
        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuOrderRequired")]
        [Display(Name = "MenuOrder", ResourceType = typeof(NesResource))]
        public int Order { get; set; }

        [Display(Name = "MenuGroupID", ResourceType = typeof(NesResource))]
        public string GroupID { get; set; }

        [Display(Name = "MenuTypeStatus", ResourceType = typeof(Resources.NesResource))]
        public bool Status { get; set; }
    }
}
