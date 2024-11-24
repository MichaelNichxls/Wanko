using System;
using System.Runtime.InteropServices;

namespace Wanko.Runtime.Native
{
    internal static partial class User32
    {
        [DllImport(nameof(User32), SetLastError = true)]
        public static extern IntPtr GetActiveWindow();

        [Obsolete]
        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LockSetForegroundWindow(LockSetForegroundWindowFlags uLockCode);

        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport(nameof(User32), SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, WindowLongIndexFlags nIndex, SetWindowLongFlags dwNewLong);

        [DllImport(nameof(User32), SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, WindowLongIndexFlags nIndex);

        [DllImport(nameof(User32), SetLastError = true, EntryPoint = "SetWindowLongPtr")]
        private static extern unsafe void* SetWindowLongPtr64(IntPtr hWnd, WindowLongIndexFlags nIndex, void* dwNewLong);

        [DllImport(nameof(User32), SetLastError = true, EntryPoint = "GetWindowLongPtr")]
        private static extern unsafe void* GetWindowLongPtr64(IntPtr hWnd, WindowLongIndexFlags nIndex);

        public static unsafe void* SetWindowLongPtr(IntPtr hWnd, WindowLongIndexFlags nIndex, void* dwNewLong) =>
            IntPtr.Size == 8
                ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
                : (void*)SetWindowLong(hWnd, nIndex, (SetWindowLongFlags)(int)dwNewLong);

        public static unsafe void* GetWindowLongPtr(IntPtr hWnd, WindowLongIndexFlags nIndex) =>
            IntPtr.Size == 8
                ? GetWindowLongPtr64(hWnd, nIndex)
                : (void*)GetWindowLong(hWnd, nIndex);
    }
}