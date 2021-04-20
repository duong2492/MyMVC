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
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "MenuID", ResourceType = typeof(NesResource))]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "AdminFunctionIdRegexMsg")]
        public string ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuNameLong")]
        [Display(Name = "MenuName", ResourceType = typeof(Resources.NesResource))]
        public string Name { get; set; }
        
        [Display(Name = "MenuDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "LinkTextRequired")]
        [StringLength(250, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "LinkTextLong")]
        [Display(Name = "LinkText", ResourceType = typeof(Resources.NesResource))]
        public string Text { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuUrlRequired")]
        [Display(Name = "MenuUrl", ResourceType = typeof(NesResource))]
        public string Link { get; set; }

        public string Target { get; set; }
        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuOrderRequired")]
        [Display(Name = "MenuOrder", ResourceType = typeof(NesResource))]
        public int Order { get; set; }
        [Display(Name = "MenuCssClass", ResourceType = typeof(NesResource))]
        public string CssClass { set; get; }


        [Display(Name = "MenuLock", ResourceType = typeof(NesResource))]
        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }
        [Display(Name = "MenuGroupID", ResourceType = typeof(NesResource))]
        public string GroupID { get; set; }

        [Display(Name = "MenuParentIDName", ResourceType = typeof(NesResource))]
        public string ParentID { get; set; }
        [Display(Name = "MenuCreatedDate", ResourceType = typeof(NesResource))]
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Language { get; set; }
    }
}
