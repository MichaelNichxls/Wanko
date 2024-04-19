using System;
using System.Runtime.InteropServices;
using static Wanko.Runtime.Native.UxTheme;

namespace Wanko.Runtime.Native
{
    internal static partial class DwmApi
    {
        [DllImport(nameof(DwmApi))]
        public static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    }
}