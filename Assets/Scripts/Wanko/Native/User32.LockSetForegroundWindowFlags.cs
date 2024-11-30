using System;

namespace Wanko.Native
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