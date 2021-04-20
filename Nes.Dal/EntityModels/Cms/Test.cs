using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nes.Dal.EntityModels
{
    [Table("Tests")]
    public class Test
    {
        [Key]
        [Display(Name = "TestID", ResourceType = typeof(Resources.NesResource))]
        public long ID { get; set; }

        [Display(Name = "TestTitle", ResourceType = typeof(Resources.NesResource))]
        public string Title { get; set; }
    }
}
