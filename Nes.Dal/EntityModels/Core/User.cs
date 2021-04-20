using Nes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nes.Dal.EntityModels
{
    [Table("Users")]
    public partial class User
    {
        [Key]
        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "UserNameRequired")]
        [StringLength(20, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "UserNameLong")]
        [Display(Name = "UserNameText", ResourceType = typeof(Resources.NesResource))]
        public string UserName { get; set; }


        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "PasswordRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "PasswordMinLong")]
        [Display(Name = "Password", ResourceType = typeof(Resources.NesResource))]
        public string Password { get; set; }


        [Display(Name = "UserPasswordLevel2", ResourceType = typeof(Resources.NesResource))]
        public string PasswordLevel2 { get; set; }


        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.NesResource))]
        public string Email { get; set; }


        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "MobileRequired")]
        [Display(Name = "Mobile", ResourceType = typeof(Resources.NesResource))]
        public string Mobile { get; set; }


        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "FullNameRequired")]
        [Display(Name = "FullName", ResourceType = typeof(Resources.NesResource))]
        public string Name { get; set; }


        [Display(Name = "Address", ResourceType = typeof(Resources.NesResource))]
        public string Address { get; set; }


        [Display(Name = "Sex", ResourceType = typeof(Resources.NesResource))]
        public bool Sex { get; set; }


        public decimal Point { get; set; }

        public string Token { get; set; }


        [Required]
        [Display(Name = "UserCreatedDate", ResourceType = typeof(Resources.NesResource))]
        public System.DateTime CreatedDate { get; set; }


        [Required]
        [Display(Name = "UserCreatedBy", ResourceType = typeof(Resources.NesResource))]
        public string CreatedBy { get; set; }


        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }


        [Required]
        [Display(Name = "UserIsLocked", ResourceType = typeof(Resources.NesResource))]
        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastChangePassword { get; set; }

    }
}
