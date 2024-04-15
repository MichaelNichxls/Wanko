using System;

namespace Wanko.Native
{
    partial class User32
    {
        internal static class SpecialWindowHandles
        {
            internal static readonly IntPtr HWND_BOTTOM     = new(1);
            internal static readonly IntPtr HWND_NOTOPMOST  = new(-2);
            internal static readonly IntPtr HWND_TOP        = new(0);
            internal static readonly IntPtr HWND_TOPMOST    = new(-1);
        }
    }
}