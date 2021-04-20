using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nes.Dal.EntityModels
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [Display(Name = "FeedbackID", ResourceType = typeof(Resources.NesResource))]
        public long ID { set; get; }
        [Required]
        [Display(Name = "FeedbackName", ResourceType = typeof(Resources.NesResource))]
        public string Name { set; get; }
        [Display(Name = "FeedbackCompany", ResourceType = typeof(Resources.NesResource))]
        public string Company { set; get; }
        [Display(Name = "FeedbackAddress", ResourceType = typeof(Resources.NesResource))]
        public string Address { set; get; }
        [Display(Name = "FeedbackPhone", ResourceType = typeof(Resources.NesResource))]
        public string Phone { set; get; }
        [Display(Name = "FeedbackEmail", ResourceType = typeof(Resources.NesResource))]
        [Required]
        public string Email { set; get; }
        [Display(Name = "FeedbackTitle", ResourceType = typeof(Resources.NesResource))]
        public string Title { set; get; }
        [Display(Name = "FeedbackMessage", ResourceType = typeof(Resources.NesResource))]
        [Required]
        public string Message { set; get; }
        [Display(Name = "FeedbackCreatedDate", ResourceType = typeof(Resources.NesResource))]
        [Required]
        public DateTime CreatedDate { set; get; }
        [Display(Name = "FeedbackIsReaded", ResourceType = typeof(Resources.NesResource))]
        [Required]
        public bool IsReaded { set; get; }
    }
}
