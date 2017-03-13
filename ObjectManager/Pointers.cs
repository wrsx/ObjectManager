using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectManager
{
    //All we need for now to find player hp...
    class Pointers
    {
        internal enum StaticPointers
        {
            LocalPlayerGUID = 0x741e30,
        }

        internal enum ObjectManager
        {
            CurMgrPointer = 0x00741414,
            FirstObject = 0xAC,
            NextObject = 0x3c,
        }

        internal enum WoWObject
        {
            DataPTR = 0x8,
            Guid = 0x30,
        }
    }
}
