using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nes.Dal.EntityModels
{
    [Table("NewsTags")]
    public class NewsTag
    {
        [Key]
        [Column(Order = 1)]
        public long NewsID { set; get; }
        [Key]
        [Column(Order =2)]
        public string TagID { set; get; }
    }
}
