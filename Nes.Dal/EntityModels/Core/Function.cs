using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nes.Resources;
namespace Nes.Dal.EntityModels
{

    [Table("Functions")]
    public partial class Function
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "FunctionID", ResourceType = typeof(NesResource))]
        [RegularExpression("^([a-zA-Z0-9.]+)$", ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "AdminFunctionIdRegexMsg")]
        public string ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FunctionNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FunctionNameLong")]
        [Display(Name = "FunctionName", ResourceType = typeof(Resources.NesResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FunctionDescriptionRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FunctionDescriptionLong")]
        [Display(Name = "FunctionDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "LinkTextRequired")]
        [StringLength(250, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "LinkTextLong")]
        [Display(Name = "LinkText", ResourceType = typeof(Resources.NesResource))]
        public string Text { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FunctionUrlRequired")]
        [Display(Name = "FunctionUrl", ResourceType = typeof(NesResource))]
        public string Link { get; set; }

        public string Target { get; set; }
        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FunctionOrderRequired")]
        [Display(Name = "FunctionOrder", ResourceType = typeof(NesResource))]
        public int Order { get; set; }
        [Display(Name = "FunctionCssClass", ResourceType = typeof(NesResource))]
        public string CssClass { set; get; }


        [Display(Name = "FunctionLock", ResourceType = typeof(NesResource))]
        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }
        [Display(Name = "FunctionParentIDName", ResourceType = typeof(NesResource))]
        public string ParentID { get; set; }
        [Display(Name = "FunctionCreatedDate", ResourceType = typeof(NesResource))]
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}
