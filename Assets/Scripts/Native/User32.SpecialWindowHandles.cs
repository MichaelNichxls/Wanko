using System;

namespace Wanko.Native
{
    partial class User32
    {
        public static class SpecialWindowHandles
        {
            public static readonly IntPtr HWND_BOTTOM     = new(1);
            public static readonly IntPtr HWND_NOTOPMOST  = new(-2);
            public static readonly IntPtr HWND_TOP        = new(0);
            public static readonly IntPtr HWND_TOPMOST    = new(-1);
        }
    }
}