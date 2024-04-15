using System.Runtime.InteropServices;

namespace Wanko.Native
{
    partial class UxTheme
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct MARGINS
        {
            internal int cxLeftWidth;
            internal int cxRightWidth;
            internal int cyTopHeight;
            internal int cyBottomHeight;
        }
    }
}