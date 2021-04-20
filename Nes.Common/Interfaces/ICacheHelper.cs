using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nes.Common.Interfaces
{
    public interface ICacheHelper
    {
        object Get(string key);
        void Set(string key, object data, int cacheTime=60);
        bool IsSet(string key);
        void Invalidate(string key);
    }
}
