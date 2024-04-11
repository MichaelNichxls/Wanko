using System;

namespace Wanko.Native
{
    internal static class NativeConstants
    {
        internal const int GWL_EXSTYLE      = -20;
        internal const int GWL_HINSTANCE    = -6;
        internal const int GWL_ID           = -12;
        internal const int GWL_STYLE        = -16;
        internal const int GWL_USERDATA     = -21;
        internal const int GWL_WNDPROC      = -4;

        internal const uint WS_EX_ACCEPTFILES           = 0x00000010;
        internal const uint WS_EX_APPWINDOW             = 0x00040000;
        internal const uint WS_EX_CLIENTEDGE            = 0x00000200;
        internal const uint WS_EX_COMPOSITED            = 0x02000000;
        internal const uint WS_EX_CONTEXTHELP           = 0x00000400;
        internal const uint WS_EX_CONTROLPARENT         = 0x00010000;
        internal const uint WS_EX_DLGMODALFRAME         = 0x00000001;
        internal const uint WS_EX_LAYERED               = 0x00080000;
        internal const uint WS_EX_LAYOUTRTL             = 0x00400000;
        internal const uint WS_EX_LEFT                  = 0x00000000;
        internal const uint WS_EX_LEFTSCROLLBAR         = 0x00004000;
        internal const uint WS_EX_LTRREADING            = 0x00000000;
        internal const uint WS_EX_MDICHILD              = 0x00000040;
        internal const uint WS_EX_NOACTIVATE            = 0x08000000;
        internal const uint WS_EX_NOINHERITLAYOUT       = 0x00100000;
        internal const uint WS_EX_NOPARENTNOTIFY        = 0x00000004;
        internal const uint WS_EX_NOREDIRECTIONBITMAP   = 0x00200000;
        internal const uint WS_EX_OVERLAPPEDWINDOW      = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE;
        internal const uint WS_EX_PALETTEWINDOW         = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST;
        internal const uint WS_EX_RIGHT                 = 0x00001000;
        internal const uint WS_EX_RIGHTSCROLLBAR        = 0x00000000;
        internal const uint WS_EX_RTLREADING            = 0x00002000;
        internal const uint WS_EX_STATICEDGE            = 0x00020000;
        internal const uint WS_EX_TOOLWINDOW            = 0x00000080;
        internal const uint WS_EX_TOPMOST               = 0x00000008;
        internal const uint WS_EX_TRANSPARENT           = 0x00000020;
        internal const uint WS_EX_WINDOWEDGE            = 0x00000100;

        internal const uint SWP_ASYNCWINDOWPOS  = 0x4000;
        internal const uint SWP_DEFERERASE      = 0x2000;
        internal const uint SWP_DRAWFRAME       = 0x0020;
        internal const uint SWP_FRAMECHANGED    = 0x0020;
        internal const uint SWP_HIDEWINDOW      = 0x0080;
        internal const uint SWP_NOACTIVATE      = 0x0010;
        internal const uint SWP_NOCOPYBITS      = 0x0100;
        internal const uint SWP_NOMOVE          = 0x0002;
        internal const uint SWP_NOOWNERZORDER   = 0x0200;
        internal const uint SWP_NOREDRAW        = 0x0008;
        internal const uint SWP_NOREPOSITION    = 0x0200;
        internal const uint SWP_NOSENDCHANGING  = 0x0400;
        internal const uint SWP_NOSIZE          = 0x0001;
        internal const uint SWP_NOZORDER        = 0x0004;
        internal const uint SWP_SHOWWINDOW      = 0x0040;   

        internal static readonly IntPtr HWND_BOTTOM     = new(1);
        internal static readonly IntPtr HWND_NOTOPMOST  = new(-2);
        internal static readonly IntPtr HWND_TOP        = new(0);
        internal static readonly IntPtr HWND_TOPMOST    = new(-1);
    }
}