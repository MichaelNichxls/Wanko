using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Wanko.Runtime.Utilities;
#if !UNITY_EDITOR
using Wanko.Runtime.Native;
using static Wanko.Runtime.Native.User32.SetWindowLongFlags;
using static Wanko.Runtime.Native.User32.WindowLongIndexFlags;
#endif

namespace Wanko.Runtime.Window
{
    [DisallowMultipleComponent]
    public sealed class WindowManager : MonoBehaviourSingleton<WindowManager>
    {
        private IEnumerable<IWindowClickthroughHandler> _clickthroughHandlers;

#pragma warning disable IDE1006
        public IntPtr hWnd
#pragma warning restore IDE1006
#if !UNITY_EDITOR
            { get; private set; }
#else
            => throw new NotSupportedException("Cannot retrieve window handle while in Unity Editor");
#endif
        protected override void Awake()
        {
            base.Awake();

            _clickthroughHandlers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IWindowClickthroughHandler>();
#if !UNITY_EDITOR
            hWnd = User32.GetActiveWindow();
#endif
        }

        private void LateUpdate() =>
            SetClickthrough(!_clickthroughHandlers.Any(handler => !handler.SetClickthrough(Mouse.current.delta.ReadValue())));

        // TODO: Optimize
        public unsafe void SetClickthrough(bool clickthrough)
        {
#if !UNITY_EDITOR
            User32.SetWindowLongFlags dwLong = (User32.SetWindowLongFlags)(int)User32.GetWindowLongPtr(hWnd, GWL_EXSTYLE);
            dwLong = clickthrough
                ? dwLong | WS_EX_TRANSPARENT
                : dwLong & ~WS_EX_TRANSPARENT;

            User32.SetWindowLongPtr(hWnd, GWL_EXSTYLE, (void*)(int)dwLong);
#endif
        }
    }
}