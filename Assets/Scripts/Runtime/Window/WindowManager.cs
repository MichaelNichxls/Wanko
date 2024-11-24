using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Wanko.Runtime.Native;
using Wanko.Runtime.Utilities;
using static Wanko.Runtime.Native.User32.SetWindowLongFlags;
using static Wanko.Runtime.Native.User32.SpecialWindowHandles;
using static Wanko.Runtime.Native.User32.WindowLongIndexFlags;
using static Wanko.Runtime.Native.UxTheme;

namespace Wanko.Runtime.Window
{
    [DisallowMultipleComponent]
    public sealed class WindowManager : MonoBehaviourSingleton<WindowManager>
    {
        private IEnumerable<IWindowTransparentHandler> _transparentHandlers;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Consistency")]
        public IntPtr hWnd { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);

            _transparentHandlers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IWindowTransparentHandler>();
#if !UNITY_EDITOR
            hWnd = User32.GetActiveWindow();
#else
            hWnd = IntPtr.Zero;
#endif
        }

        // TODO: Add lines like: Camera.main.backgroundColor = Color.clear;
        private unsafe void Start()
        {
            if (hWnd == IntPtr.Zero)
                return;

            MARGINS margins = new() { cxLeftWidth = -1 };

            _ = DwmApi.DwmExtendFrameIntoClientArea(hWnd, ref margins);
            _ = User32.SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
            _ = User32.SetWindowLongPtr(hWnd, GWL_EXSTYLE, (void*)(int)WS_EX_LAYERED);
            SetTransparent(true);
        }

        private void LateUpdate() =>
            SetTransparent(!_transparentHandlers.Any(handler => !handler.SetTransparent(Mouse.current.position.ReadValue())));

        // TODO: Optimize and make conversion helper. Calling this every frame is probably bad.
        public unsafe void SetTransparent(bool transparent)
        {
            if (hWnd == IntPtr.Zero)
                return;

            User32.SetWindowLongFlags dwLong = (User32.SetWindowLongFlags)(int)User32.GetWindowLongPtr(hWnd, GWL_EXSTYLE);
            dwLong = transparent
                ? dwLong | WS_EX_TRANSPARENT
                : dwLong & ~WS_EX_TRANSPARENT;

            User32.SetWindowLongPtr(hWnd, GWL_EXSTYLE, (void*)(int)dwLong);
        }
    }
}