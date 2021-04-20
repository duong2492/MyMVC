using Nes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nes.Dal.EntityModels
{
     [Table("Albums")]
    public class Album
    {
        [Key]
        [Display(Name = "AlbumID", ResourceType = typeof(Resources.NesResource))]
        public long ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(NesResource), ErrorMessageResourceName = "AlbumNameRequired")]
        [Display(Name = "AlbumName", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }

        public string MetaTitle { get; set; }

        [Display(Name = "AlbumImages", ResourceType = typeof(Resources.NesResource))]
        public string Images { get; set; }

        [Display(Name = "AlbumDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { get; set; }


        [Display(Name = "AlbumOrder", ResourceType = typeof(Resources.NesResource))]
        public int Order { get; set; }

        [Required]
        [Display(Name = "AlbumCreatedDate", ResourceType = typeof(NesResource))]
        public System.DateTime CreatedDate { get; set; }
        [Display(Name = "AlbumCreatedBy", ResourceType = typeof(NesResource))]
        public string CreatedBy { get; set; }
        
        [Display(Name = "AlbumMetaKeywords", ResourceType = typeof(Resources.NesResource))]
        public string MetaKeywords { get; set; }

        [Display(Name = "AlbumMetaDescription", ResourceType = typeof(Resources.NesResource))]
        public string MetaDescription { get; set; }

        [Display(Name = "AlbumStatus", ResourceType = typeof(Resources.NesResource))]
        public bool Status { get; set; }

        [Display(Name = "AlbumLanguageCode", ResourceType = typeof(NesResource))]
        public string LanguageCode { get; set; }
    }
}
