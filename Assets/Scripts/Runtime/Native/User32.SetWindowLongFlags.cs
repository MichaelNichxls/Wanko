using System;

namespace Wanko.Runtime.Native
{
    partial class User32
    {
        [Flags]
        public enum SetWindowLongFlags : uint
        {
            WS_OVERLAPPED   = 0,
            WS_POPUP        = 0x8000_0000,
            WS_CHILD        = 0x4000_0000,
            WS_MINIMIZE     = 0x2000_0000,
            WS_VISIBLE      = 0x1000_0000,
            WS_DISABLED     = 0x800_0000,
            WS_CLIPSIBLINGS = 0x400_0000,
            WS_CLIPCHILDREN = 0x200_0000,
            WS_MAXIMIZE     = 0x100_0000,
            WS_CAPTION      = 0xC0_0000,
            WS_BORDER       = 0x80_0000,
            WS_DLGFRAME     = 0x40_0000,
            WS_VSCROLL      = 0x20_0000,
            WS_HSCROLL      = 0x10_0000,
            WS_SYSMENU      = 0x8_0000,
            WS_THICKFRAME   = 0x4_0000,
            WS_GROUP        = 0x2_0000,
            WS_TABSTOP      = 0x1_0000,
            WS_MINIMIZEBOX  = 0x2_0000,
            WS_MAXIMIZEBOX  = 0x1_0000,
            WS_TILED        = WS_OVERLAPPED,
            WS_ICONIC       = WS_MINIMIZE,
            WS_SIZEBOX      = WS_THICKFRAME,

            WS_EX_DLGMODALFRAME     = 0x0001,
            WS_EX_NOPARENTNOTIFY    = 0x0004,
            WS_EX_TOPMOST           = 0x0008,
            WS_EX_ACCEPTFILES       = 0x0010,
            WS_EX_TRANSPARENT       = 0x0020,
            WS_EX_MDICHILD          = 0x0040,
            WS_EX_TOOLWINDOW        = 0x0080,
            WS_EX_WINDOWEDGE        = 0x0100,
            WS_EX_CLIENTEDGE        = 0x0200,
            WS_EX_CONTEXTHELP       = 0x0400,
            WS_EX_RIGHT             = 0x1000,
            WS_EX_LEFT              = 0x0000,
            WS_EX_RTLREADING        = 0x2000,
            WS_EX_LTRREADING        = 0x0000,
            WS_EX_LEFTSCROLLBAR     = 0x4000,
            WS_EX_RIGHTSCROLLBAR    = 0x0000,
            WS_EX_CONTROLPARENT     = 0x1_0000,
            WS_EX_STATICEDGE        = 0x2_0000,
            WS_EX_APPWINDOW         = 0x4_0000,
            WS_EX_OVERLAPPEDWINDOW  = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
            WS_EX_PALETTEWINDOW     = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
            WS_EX_LAYERED           = 0x0008_0000,
            WS_EX_NOINHERITLAYOUT   = 0x0010_0000,
            WS_EX_LAYOUTRTL         = 0x0040_0000,
            WS_EX_COMPOSITED        = 0x0200_0000,
            WS_EX_NOACTIVATE        = 0x0800_0000
        }
    }
}