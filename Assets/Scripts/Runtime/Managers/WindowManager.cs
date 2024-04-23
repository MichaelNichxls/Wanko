using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if !UNITY_EDITOR
using UnityEngine.InputSystem;
using Wanko.Runtime.Native;
using static Wanko.Runtime.Native.User32.SetWindowLongFlags;
using static Wanko.Runtime.Native.User32.WindowLongIndexFlags;
#endif

namespace Wanko.Runtime.Managers
{
    [DisallowMultipleComponent]
    public sealed class WindowManager : MonoBehaviour
    {
#pragma warning disable IDE0052
        private IEnumerable<IWindowClickthroughHandler> _clickthroughHandlers;
#pragma warning restore IDE0052

        public static WindowManager Instance { get; private set; }
#pragma warning disable IDE1006
        public IntPtr hWnd
#pragma warning restore IDE1006
#if !UNITY_EDITOR
            { get; private set; }
#else
            => throw new NotSupportedException("Cannot retrieve window handle while in Unity Editor");
#endif
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            _clickthroughHandlers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IWindowClickthroughHandler>();

            Instance = this;
#if !UNITY_EDITOR
            hWnd = User32.GetActiveWindow();
#endif
        }
#if !UNITY_EDITOR
        private void LateUpdate() =>
            SetClickthrough(!_clickthroughHandlers.Any(handler => !handler.SetClickthrough(Mouse.current.delta.ReadValue())));
#endif
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