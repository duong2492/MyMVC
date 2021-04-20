using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nes.Common
{
    public enum NotificationEnumeration
    {
        Success,
        Error,
        Warning
    }
    public class StatusEnumeration
    {
        public static int Published = 1;
        public static int Waiting = 2;
        public static int Unpublished = 3;
    }
}
