using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectManager
{
    //All we need for now to find player hp...
    class Descriptors
    {
        internal enum WoWUnitFields {
            Health = 0x58 / 4
        }
    }
}
