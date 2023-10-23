using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Wanko
{
    public sealed class TransparentWindow : MonoBehaviour
    {
        // TODO: Move everything below into a folder called "Native"
        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        private const int GWL_EXSTYLE           = -20;
        private const uint WS_EX_LAYERED        = 0x00080000;
        private const uint WS_EX_TRANSPARENT    = 0x00000020;
        private const uint LWA_COLORKEY         = 0x00000001;

        private static readonly IntPtr HWND_TOPMOST = new(-1);

        private IntPtr _hWnd;
        
        [DllImport("dwmapi.dll")]
        private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        private static extern int SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte bAlpha, uint dwFlags);

        private void Start()
        {
#if !UNITY_EDITOR
            _hWnd = GetActiveWindow();
            MARGINS margins = new() { cxLeftWidth = -1 };

            DwmExtendFrameIntoClientArea(_hWnd, ref margins);
            SetWindowLong(_hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
            SetLayeredWindowAttributes(_hWnd, 0, 0, LWA_COLORKEY);
            SetWindowPos(_hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
#endif
        }

        private void Update()
        {
#if !UNITY_EDITOR && false
            // TODO: Make folder called "Utilities"
            uint dwNewLong = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) is null
                ? WS_EX_LAYERED | WS_EX_TRANSPARENT
                : WS_EX_LAYERED;

            //var raycaster = GetComponent<CubismRaycaster>();
            //// Get up to 4 results of collision detection.
            //var results = new CubismRaycastHit[1];
            //// Cast ray from pointer position.
            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //var hitCount = raycaster.Raycast(ray, results);

            //uint dwNewLong = hitCount > 0
            //    ? WS_EX_LAYERED | WS_EX_TRANSPARENT
            //    : WS_EX_LAYERED;

            SetWindowLong(_hWnd, GWL_EXSTYLE, dwNewLong);
#endif
        }
    }
}