using System;
using System.Runtime.InteropServices;

namespace Wanko.Native
{
    internal static partial class User32
    {
        [DllImport(nameof(User32), SetLastError = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport(nameof(User32), SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, WindowLongIndexFlags nIndex, SetWindowLongFlags dwNewLong);

        [DllImport(nameof(User32), SetLastError = true, EntryPoint = "SetWindowLongPtr")]
        private static extern unsafe void* SetWindowLongPtr64(IntPtr hWnd, WindowLongIndexFlags nIndex, void* dwNewLong);

        public static unsafe void* SetWindowLongPtr(IntPtr hWnd, WindowLongIndexFlags nIndex, void* dwNewLong) =>
            IntPtr.Size == 8
                ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
                : (void*)SetWindowLong(hWnd, nIndex, (SetWindowLongFlags)(int)dwNewLong);
    }
}