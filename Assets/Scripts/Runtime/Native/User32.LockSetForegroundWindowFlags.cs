using System;

namespace Wanko.Runtime.Native
{
    partial class User32
    {
        [Obsolete]
        public enum LockSetForegroundWindowFlags : uint
        {
            LSFW_LOCK   = 1,
            LSFW_UNLOCK = 2
        }
    }
}