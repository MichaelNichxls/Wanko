using System.Runtime.InteropServices;

namespace Wanko.Runtime.Native
{
    partial class UxTheme
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }
    }
}