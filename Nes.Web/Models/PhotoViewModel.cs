using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nes.Web.Models
{
    public class PhotoViewModel
    {
        public IEnumerable<Photo> PhotoList { set; get; }
        public Album Album { set; get; }
    }
}