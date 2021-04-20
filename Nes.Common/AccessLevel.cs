using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nes.Common
{
    public enum AccessLevel
    {
        Create,
        Edit,
        Delete,
        List,
        Full = (Create | Edit | Delete | List)
    }
}
