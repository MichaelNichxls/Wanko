using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
#if !UNITY_EDITOR
using Wanko.Runtime.Native;
using static Wanko.Runtime.Native.User32.SetWindowLongFlags;
using static Wanko.Runtime.Native.User32.WindowLongIndexFlags;
#endif

namespace Wanko.Runtime.Managers
{
    [DisallowMultipleComponent]
    public sealed class WindowManager : MonoBehaviour, ApplicationInputActions.IWindowActions
    {
        private ApplicationInputActions.WindowActions _actions;
#pragma warning disable IDE0052
        private IEnumerable<IWindowClickthroughHandler> _clickthroughHandlers;
#pragma warning restore IDE0052

        public static WindowManager Instance { get; private set; }
#if !UNITY_EDITOR
        public IntPtr HWnd { get; private set; }
#else
        // do this everywhere
        // also throw #error in native methods class maybe
        public IntPtr HWnd => throw new NotSupportedException("Cannot retrieve window handle while in Unity Editor");
#endif
        void ApplicationInputActions.IWindowActions.OnPosition(InputAction.CallbackContext context)
        {
#if !UNITY_EDITOR
            if (!context.performed)
                return;
            
            SetClickthrough(!_clickthroughHandlers.Any(handler => !handler.SetClickthrough(context.ReadValue<Vector2>())));
#endif
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            _actions = new ApplicationInputActions().Window;
            _clickthroughHandlers = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IWindowClickthroughHandler>();
            
            Instance = this;
#if !UNITY_EDITOR
            HWnd = User32.GetActiveWindow();
#endif
        }

        private void OnEnable()
        {
            _actions.Enable();
            _actions.AddCallbacks(this);
        }

        private void OnDisable()
        {
            _actions.Disable();
            _actions.RemoveCallbacks(this);
        }
#if !UNITY_EDITOR
        // make conversion helper
        // internal?
        public unsafe void SetClickthrough(bool clickthrough)
        {
            // make 2nd arg class member
            User32.SetWindowLongFlags dwLong = (User32.SetWindowLongFlags)(int)User32.GetWindowLongPtr(HWnd, GWL_EXSTYLE);
            dwLong = clickthrough
                ? dwLong | WS_EX_TRANSPARENT
                : dwLong & ~WS_EX_TRANSPARENT;

            User32.SetWindowLongPtr(HWnd, GWL_EXSTYLE, (void*)(int)dwLong);
        }
#endif
    }
}