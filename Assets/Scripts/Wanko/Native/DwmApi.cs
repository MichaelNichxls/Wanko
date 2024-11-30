using System;
using System.Runtime.InteropServices;
using static Wanko.Native.UxTheme;

namespace Wanko.Native
{
    internal static partial class DwmApi
    {
        [DllImport(nameof(DwmApi))]
        public static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    }
}