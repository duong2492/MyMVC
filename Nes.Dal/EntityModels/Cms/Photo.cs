using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nes.Dal.EntityModels
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        [Display(Name = "PhotoID", ResourceType = typeof(Resources.NesResource))]
        public long ID { set; get; }
        [Display(Name = "PhotoName", ResourceType = typeof(Resources.NesResource))]
        public string Title { set; get; }
        [Display(Name = "PhotoImages", ResourceType = typeof(Resources.NesResource))]
        public string Images { set; get; }
        [Display(Name = "PhotoAlbumID", ResourceType = typeof(Resources.NesResource))]
        [Required]
        public long AlbumID { set; get; }
        [Display(Name = "PhotoDescription", ResourceType = typeof(Resources.NesResource))]
        public string Description { set; get; }
        [Display(Name = "PhotoCreatedDate", ResourceType = typeof(Resources.NesResource))]
        public DateTime? CreatedDate { set; get; }
        [Display(Name = "PhotoCreatedBy", ResourceType = typeof(Resources.NesResource))]
        public string CreatedBy { set; get; }
        [Display(Name = "PhotoStatus", ResourceType = typeof(Resources.NesResource))]
        public bool Status { set; get; }
    }
}
