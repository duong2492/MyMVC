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
    [Table("MenuTypes")]
    public class MenuType
    {
        [Key]
        [Display(Name = "MenuTypeId", ResourceType = typeof(Resources.NesResource))]
        public string ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuTypeNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MenuTypeNameLong")]
        [Display(Name = "MenuTypeName", ResourceType = typeof(Resources.NesResource))]
        public string Name { get; set; }
        [Display(Name = "MenuTypeDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }
        [Display(Name = "MenuTypeCreateDate", ResourceType = typeof(Resources.NesResource))]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "MenuTypeCreateBy", ResourceType = typeof(Resources.NesResource))]
        public string CreatedBy { get; set; }
        [Display(Name = "MenuTypeUpdatedDate", ResourceType = typeof(Resources.NesResource))]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name = "MenuTypeUpdatedBy", ResourceType = typeof(Resources.NesResource))]
        public string UpdatedBy { get; set; }
        [Display(Name = "MenuTypeStatus", ResourceType = typeof(Resources.NesResource))]
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
    }
}
