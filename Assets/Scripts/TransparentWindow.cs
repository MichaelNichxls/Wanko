using System;
using UnityEngine;
using UnityEngine.Events;

#if !UNITY_EDITOR
using Wanko.Native;
using static Wanko.Native.NativeConstants;
#endif

namespace Wanko
{
    [DisallowMultipleComponent]
    internal sealed class TransparentWindow : MonoBehaviour
    {
#if !UNITY_EDITOR
        private IntPtr _hWnd;
#if EXPERIMENTAL
        private IntPtr _trayWnd;
#endif
#endif
        [field: SerializeField]
        public UnityEvent<IntPtr> SetWindowLong { get; private set; }

#if !UNITY_EDITOR
        private void Awake() =>
            _hWnd = NativeMethods.GetActiveWindow();

        private void Start()
        {
            MARGINS margins = new() { cxLeftWidth = -1 };

            NativeMethods.DwmExtendFrameIntoClientArea(_hWnd, ref margins);
            NativeMethods.SetWindowPos(_hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
#if EXPERIMENTAL
            _trayWnd = NativeMethods.FindWindow("Shell_TrayWnd", null);
            if (_trayWnd == IntPtr.Zero)
                return;

            NativeMethods.SetWindowPos(_trayWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);

            // Show the taskbar window if it's hidden
            NativeMethods.ShowWindow(_trayWnd, 9);

            // Bring the taskbar window to the foreground
            NativeMethods.SetForegroundWindow(_trayWnd);
#endif
        }

        // Optimize; Make default dwNewLong configurable; Abstract and refactor
        private void LateUpdate()
        {
            NativeMethods.SetWindowLong(_hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
            SetWindowLong?.Invoke(_hWnd);
        }
#endif
        }
    }